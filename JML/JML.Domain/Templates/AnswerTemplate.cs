using System;
using JML.Domain.Core.Contracts;

namespace JML.Domain.Templates
{
    public class AnswerTemplate : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; protected set; }
        public Guid QuestionTemplateId { get; set; }

        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual QuestionTemplate Question { get; set; }
    }
}
