using System;

namespace JML.Domain.Core.Contracts
{
    public interface IAuditableEntity<TKey> : IAppEntity<TKey>
    {
        DateTime CreatedAt { get; }
        DateTime ModifiedAt { get; }
    }
}
