using JML.Domain.Enums;
using System;
using System.Collections.Generic;

namespace JML.ApiModels
{
    public class QuestionTemplateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ControlType ControlType { get; set; }
        public List<AnswerTemplateModel> Answers { get; set; }
    }
}
