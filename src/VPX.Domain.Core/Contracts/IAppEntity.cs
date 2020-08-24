namespace VPX.Domain.Core.Contracts
{
    public interface IAppEntity<TKey>
    {
        TKey Id { get; }
    }
}
