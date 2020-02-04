namespace JML.Domain.Core.Contracts
{
    public interface IAppEntity<TKey>
    {
        TKey Id { get; }
    }
}
