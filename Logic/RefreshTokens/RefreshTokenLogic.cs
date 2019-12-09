using AutoMapper;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Generic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.RefreshTokens
{
    class RefreshTokenLogic : GenericLogic<IRepository<RefreshToken>, RefreshTokenDto, RefreshToken>, IRefreshTokenLogic
    {
        private IRepository<RefreshToken> _refreshTokenRepository;
        public RefreshTokenLogic(IRepository<RefreshToken> repository, IRepository<RefreshToken> refreshTokenRepository, ILogger<RefreshTokenLogic> logger) : base(repository, logger)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public RefreshTokenDto GetToken(string token)
        {
            var storedToken = _refreshTokenRepository.GetSingle(x => x.Token == token);
            var mappedToken = Mapper.Map<RefreshTokenDto>(storedToken);
            return mappedToken;
        }

        public bool Update(string entityId, RefreshTokenDto entity)
        {
            var success = false;
            if (entity != null)
                using (var scope = Repository.DatabaseFacade.BeginTransaction())
                {
                    var refreshToken = Repository.GetByID(entityId);
                    if (refreshToken != null)
                    {
                        try
                        {

                            var item = Mapper.Map(entity, refreshToken);
                            Repository.Update(item);
                            Repository.SaveChanges();
                            scope.Commit();
                            success = true;
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
            return success;
        }
    }
}
