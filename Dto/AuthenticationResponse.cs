using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
