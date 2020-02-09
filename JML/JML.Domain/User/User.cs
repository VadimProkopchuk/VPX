using System;
using JML.Domain.Core.Contracts;

namespace JML.Domain.User
{
    public class User : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
