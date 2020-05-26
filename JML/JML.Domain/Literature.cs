using JML.Domain.Core.Contracts;
using System;

namespace JML.Domain
{
    public class Literature : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
