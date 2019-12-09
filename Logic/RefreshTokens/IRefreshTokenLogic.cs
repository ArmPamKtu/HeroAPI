using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.RefreshTokens
{
    public interface IRefreshTokenLogic
    {
        RefreshTokenDto GetToken(string token);
        bool Update(string entityId, RefreshTokenDto entity);

        RefreshTokenDto Create(RefreshTokenDto entity);
    }
}
