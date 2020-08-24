using System;

namespace VPX.ApiModels
{
    public class TokenPair
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
