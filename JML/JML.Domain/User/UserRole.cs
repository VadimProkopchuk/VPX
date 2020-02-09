using JML.Domain.Core.Contracts;
using JML.Domain.Enums;
using System;

namespace JML.Domain.User
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
