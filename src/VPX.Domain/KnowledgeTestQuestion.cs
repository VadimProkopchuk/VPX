using System;
using VPX.Domain.Core.Contracts;
using VPX.Domain.Templates;

namespace VPX.Domain
{
    public class KnowledgeTestQuestion : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; protected set; }
        public Guid QuestionTemplateId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsProvidedCorrectAnswer { get; set; }

        public virtual QuestionTemplate QuestionTemplate { get; set; }
    }
}
