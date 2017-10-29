using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gateway.Api.Models
{
    [Serializable]
    public class JwtPayload
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("claims")]
        public IEnumerable<JwtClaim> JwtClaims { get; set; }
    }
}