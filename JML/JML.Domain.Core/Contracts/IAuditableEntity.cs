using System;

namespace JML.Domain.Core.Contracts
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}
