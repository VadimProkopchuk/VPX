namespace VPX.BusinessLogic.Core.Contracts.Systems
{
    public interface IPasswordGenerator
    {
        string Generate(int length);
    }
}
