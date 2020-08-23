using JML.Domain.Core.Contracts;
using JML.Domain.Templates;
using System;
using System.Collections.Generic;

namespace JML.Domain
{
    public class KnowledgeTest : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; protected set; }
        public Guid TestTemplateId { get; set; }
        public Guid UserId { get; set; }

        public DateTime ExpiredAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual TestTemplate TestTemplate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<KnowledgeTestQuestion> Questions { get; set; }
    }
}
