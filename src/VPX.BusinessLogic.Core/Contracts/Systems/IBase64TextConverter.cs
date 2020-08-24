namespace VPX.BusinessLogic.Core.Contracts.Systems
{
    public interface IBase64TextConverter
    {
        string ToBase64(string message);
        string ToString(string base64);
    }
}
