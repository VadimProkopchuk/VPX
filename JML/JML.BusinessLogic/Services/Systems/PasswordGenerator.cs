using System;
using JML.BusinessLogic.Core.Contracts.Systems;

namespace JML.BusinessLogic.Services.Systems
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string Generate(int length)
        {
            var validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            var random = new Random();
            var chars = new char[length];
            
            for (var i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            
            return new string(chars);
        }
    }
}
