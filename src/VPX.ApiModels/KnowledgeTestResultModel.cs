using System;

namespace VPX.ApiModels
{
    public class KnowledgeTestResultModel
    {
        public string Name { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }
        public double Result { get; set; }
        public int Mark { get; set; }
    }
}
