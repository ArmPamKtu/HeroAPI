using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Exceptions
{
    public enum ExceptionCode
    {
        Unhandled = 1000,
        NotEnoughFeats = 1001,
        IncorrectEmail = 1002,
        SendingFeatsToYourself = 1003,
        FeatAlreadySentToThisUser = 1004,
        FiredEmployee = 1005,
        DuplicateEmails = 1008,
        InvalidFeatValue = 1009,
        Unauthorised = 1010,
        DuplicateProductNames = 1011,
        ProductVersionDoesNotExist = 1012,
        EmailAlreadyExists = 1013,
        EmailDoesNotExist = 1014,
        IncorrectPassword = 1015,
        InvalidToken = 1016,
        TokenIsNotExpired = 1017,
        RefreshTokenDoesNotExist = 1018,
        RefreshTokenHasExpired = 1019,
        RefreshTokenInvalidated = 1020,
        RefreshTokenUsed = 1021,
        RefreshTokenDoesNotMatchJWT = 1022,
        UserDoesNotExist = 1023,
        RoleDoesNotExist = 1024
    }
}
