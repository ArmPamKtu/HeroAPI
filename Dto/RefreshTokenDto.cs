using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public string UserId { get; set; }
    }
}
