namespace JML.BusinessLogic.Core.Contracts.Systems
{
    public interface IPasswordGenerator
    {
        string Generate(int length);
    }
}
