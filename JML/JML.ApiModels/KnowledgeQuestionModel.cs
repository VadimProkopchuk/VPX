using JML.Domain.Enums;
using System;
using System.Collections.Generic;

namespace JML.ApiModels
{
    public class KnowledgeQuestionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ControlType ControlType { get; set; }
        public Guid? AnswerId { get; set; }
        public List<KnowledgeAnswerModel> Answers { get; set; }
    }
}
