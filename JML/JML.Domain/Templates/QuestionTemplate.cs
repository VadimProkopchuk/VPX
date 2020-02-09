using System;
using System.Collections.Generic;
using JML.Domain.Core.Contracts;
using JML.Domain.Enums;

namespace JML.Domain.Templates
{
    public class QuestionTemplate : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; protected set; }
        public Guid TestTemplateId { get; set; }

        public string Name { get; set; }
        public ControlType ControlType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual TestTemplate Test { get; set; }
        public virtual ICollection<AnswerTemplate> Answers { get; set; }
    }
}
