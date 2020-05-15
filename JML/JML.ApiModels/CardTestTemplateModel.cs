using System;

namespace JML.ApiModels
{
    public class CardTestTemplateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountOfQuestions { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? LastResult { get; set; }
        public int? Attempts { get; set; }
    }
}
