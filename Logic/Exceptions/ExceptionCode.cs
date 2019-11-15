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
        ProductVersionDoesNotExist = 1012
    }
}
