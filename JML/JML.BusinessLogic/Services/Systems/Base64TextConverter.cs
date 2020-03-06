using System;
using System.Text;
using JML.BusinessLogic.Core.Contracts.Systems;

namespace JML.BusinessLogic.Services.Systems
{
    public class Base64TextConverter : IBase64TextConverter
    {
        public string ToBase64(string message)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(message);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string ToString(string base64)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
