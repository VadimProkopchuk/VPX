using System;
using JML.Domain.Base;

namespace JML.Domain.Templates
{
    public class AnswerTemplate : BaseAuditableEntity<Guid>
    {
        protected AnswerTemplate() { }

        public AnswerTemplate(string answer, bool isCorrect)
        {
            Answer = answer;
            IsCorrect = isCorrect;
        }

        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

        public virtual QuestionTemplate Question { get; set; }
    }
}
