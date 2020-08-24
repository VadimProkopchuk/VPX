using System;
using System.Collections.Generic;

namespace VPX.ApiModels
{
    public class KnowledgeTestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<KnowledgeQuestionModel> Questions { get; set; } 
    }
}
