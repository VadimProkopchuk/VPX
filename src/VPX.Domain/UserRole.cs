using System;
using VPX.Domain.Core.Contracts;
using VPX.Enums;

namespace VPX.Domain
{
    public class UserRole : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual User User { get; set; }
    }
}
