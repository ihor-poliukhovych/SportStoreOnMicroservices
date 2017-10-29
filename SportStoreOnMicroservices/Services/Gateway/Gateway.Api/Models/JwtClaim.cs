using System;
using Newtonsoft.Json;

namespace Gateway.Api.Models
{
    [Serializable]
    public class JwtClaim
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}