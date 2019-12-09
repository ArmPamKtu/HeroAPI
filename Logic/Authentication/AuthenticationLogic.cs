using Db.Entities;
using Dto;
using Logic.Exceptions;
using Logic.RefreshTokens;
using Logic.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Authentication
{
    public class AuthenticationLogic : IAuthenticationLogic
    {
        private readonly IRefreshTokenLogic _refreshTokenLogic;
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthenticationLogic(IRefreshTokenLogic refreshTokenLogic, UserManager<User> userManager, IOptions<JwtSettings> jwtSettings,
            TokenValidationParameters tokenValidationParameters)
        {
            _refreshTokenLogic = refreshTokenLogic;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthenticationResponse> LoginAsync(UserLoginDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new BusinessException(ExceptionCode.EmailDoesNotExist);
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasValidPassword)
                throw new BusinessException(ExceptionCode.IncorrectPassword);

            var authenticationResponse = await GenerateAuthenticationResponse(user);
            return authenticationResponse;
        }

        public async Task<AuthenticationResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var validatedToken = GetPrincipalFromToken(request.Token);
            if (validatedToken == null)
                throw new BusinessException(ExceptionCode.InvalidToken);

            var expirationDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expirationDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMinutes(expirationDateUnix);

            if (expirationDateTimeUtc > DateTime.UtcNow)
                throw new BusinessException(ExceptionCode.TokenIsNotExpired);

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = _refreshTokenLogic.GetToken(request.RefreshToken);

            if (storedRefreshToken == null)
                throw new BusinessException(ExceptionCode.RefreshTokenDoesNotExist);

            if (DateTime.UtcNow > storedRefreshToken.ExpirationDate)
                throw new BusinessException(ExceptionCode.RefreshTokenHasExpired);

            if (storedRefreshToken.Invalidated)
                throw new BusinessException(ExceptionCode.RefreshTokenInvalidated);

            if (storedRefreshToken.Used)
                throw new BusinessException(ExceptionCode.RefreshTokenUsed);

            if (storedRefreshToken.JwtId != jti)
                throw new BusinessException(ExceptionCode.RefreshTokenDoesNotMatchJWT);

            storedRefreshToken.Used = true;
            _refreshTokenLogic.Update(request.RefreshToken, storedRefreshToken);

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            var authenticationResponce = await GenerateAuthenticationResponse(user);
            return authenticationResponce;
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResponse> GenerateAuthenticationResponse(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim("id", user.Id)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            // Adding roles code
            // Roles property is string collection but you can modify Select code if it it's not
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessExpiration),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDesriptor);

            var refreshToken = new RefreshTokenDto
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                Invalidated = false,
                Used = false,
                ExpirationDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshExpiration)
            };

            var createdRefreshToken = _refreshTokenLogic.Create(refreshToken);

            var jwtToken = tokenHandler.WriteToken(token);

            var authenticationResponse = new AuthenticationResponse
            {
                Token = jwtToken,
                RefreshToken = createdRefreshToken.Token
            };
            return authenticationResponse;
        }
    }
}
