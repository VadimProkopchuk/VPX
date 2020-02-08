using System;
using System.Collections.Generic;
using JML.Domain.Base;
using JML.Domain.Enums;

namespace JML.Domain.Templates
{
    public class QuestionTemplate : BaseAuditableEntity<Guid>
    {
        protected QuestionTemplate() { }

        public QuestionTemplate(string name, ControlType controlType) : this()
        {
            Name = name;
            ControlType = controlType;
        }

        public string Name { get; set; }
        public ControlType ControlType { get; set; }

        public virtual TestTemplate Test { get; set; }
        public virtual ICollection<AnswerTemplate> Answers { get; set; }
    }
}
