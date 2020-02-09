using JML.Domain.Core.Contracts;
using System;
using System.Collections.Generic;

namespace JML.Domain
{
    public class Group : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
