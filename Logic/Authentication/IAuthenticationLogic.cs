using Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Authentication
{
    public interface IAuthenticationLogic
    {
        Task<AuthenticationResponse> LoginAsync(UserLoginDto request);

        Task<AuthenticationResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
