using System;

namespace JML.Models
{
    public class JwtModel
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
