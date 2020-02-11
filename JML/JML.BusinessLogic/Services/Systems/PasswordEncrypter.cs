using JML.BusinessLogic.Core.Contracts.Systems;

namespace JML.BusinessLogic.Services.Systems
{
    public class PasswordEncrypter : IPasswordEncrypter
    {
        public string Encrypt(string message)
        {
            return message;
        }
    }
}
