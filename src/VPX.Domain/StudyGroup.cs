using System;
using System.Collections.Generic;
using VPX.Domain.Core.Contracts;

namespace VPX.Domain
{
    public class StudyGroup : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
