namespace VPX.BusinessLogic.Core.Contracts.Systems
{
    public interface IPasswordEncrypter
    {
        string Encrypt(string message);
    }
}
