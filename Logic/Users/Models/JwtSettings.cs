using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Users.Models
{
    [JsonObject("tokenManagement")]
    class JwtSettings
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}
