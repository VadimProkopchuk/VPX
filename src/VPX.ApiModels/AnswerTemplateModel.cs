using System;

namespace VPX.ApiModels
{
    public class AnswerTemplateModel
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
