using System;

namespace VPX.Models
{
    public class JwtModel
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
