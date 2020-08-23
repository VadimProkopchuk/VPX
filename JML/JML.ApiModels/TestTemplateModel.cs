using System;
using System.Collections.Generic;

namespace JML.ApiModels
{
    public class TestTemplateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountOfQuestions { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<QuestionTemplateModel> Questions { get; set; }
    }
}
