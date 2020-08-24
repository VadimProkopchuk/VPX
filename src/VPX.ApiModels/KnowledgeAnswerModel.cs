using System;

namespace VPX.ApiModels
{
    public class KnowledgeAnswerModel
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool IsSelected { get; set; }
    }
}
