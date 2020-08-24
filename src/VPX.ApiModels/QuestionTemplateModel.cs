using System;
using System.Collections.Generic;
using VPX.Enums;

namespace VPX.ApiModels
{
    public class QuestionTemplateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ControlType ControlType { get; set; }
        public List<AnswerTemplateModel> Answers { get; set; }
    }
}
