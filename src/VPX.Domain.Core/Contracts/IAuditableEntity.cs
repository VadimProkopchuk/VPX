using System;

namespace VPX.Domain.Core.Contracts
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}
