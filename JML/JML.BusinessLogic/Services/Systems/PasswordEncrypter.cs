using System;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.Models.Settings;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace JML.BusinessLogic.Services.Systems
{
    public class PasswordEncrypter : IPasswordEncrypter
    {
        private readonly byte[] salt;

        public PasswordEncrypter(IOptions<AppSettings> appSettings)
        {
            salt = string.IsNullOrEmpty(appSettings.Value.Security.PasswordSalt) 
                ? new byte[0] 
                : Convert.FromBase64String(appSettings.Value.Security.PasswordSalt);
        }

        public string Encrypt(string message)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: message,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
