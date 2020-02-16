using System;

namespace JML.ApiModels
{
    public class TokenPair
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
