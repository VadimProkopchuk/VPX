using System;
using System.Collections.Generic;

namespace JML.ApiModels
{
    public class LectureModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public string Preview { get; set; }
        public string Section { get; set; }
        public List<TagModel> Tags { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
