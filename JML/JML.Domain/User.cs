using System;
using System.Collections.Generic;
using JML.Domain.Core.Contracts;

namespace JML.Domain
{
    public class User : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid? GroupId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int CountOfInvalidAttempts { get; set; }
        public bool IsLocked { get; set; }

        public DateTime LoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
