using System;
using VPX.Domain.Core.Contracts;

namespace VPX.Domain
{
    public class Literature : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
