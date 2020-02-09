using System;

namespace JML.Domain.Core.Contracts
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; }
        DateTime ModifiedAt { get; }
    }
}
