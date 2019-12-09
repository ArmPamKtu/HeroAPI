using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
