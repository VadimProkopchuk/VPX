using JML.Domain.Core.Contracts;

namespace JML.Domain.Base
{
    public abstract class BaseAppEntity<TKey> : IAppEntity<TKey>
    {
        public TKey Id { get; protected set; }
    }
}
