using System;
using JML.Domain.Core.Contracts;

namespace JML.Domain.Base
{
    public abstract class BaseAuditableEntity<TKey> : BaseAppEntity<TKey>, IAuditableEntity
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }
    }
}
