namespace JML.BusinessLogic.Core.Contracts.Systems
{
    public interface IPasswordEncrypter
    {
        string Encrypt(string message);
    }
}
