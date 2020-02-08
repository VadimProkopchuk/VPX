using System;
using System.Collections.Generic;
using JML.Domain.Base;

namespace JML.Domain.Templates
{
    public class TestTemplate : BaseAuditableEntity<Guid>
    {
        protected TestTemplate() { }

        public TestTemplate(string name, string description, int different) : this()
        {
            Name = name;
            Description = description;
            Different = different;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Different { get; set; }
        public int GeneratedCount { get; set; }
        public TimeSpan ExecuteTime { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<QuestionTemplate> Questions { get; set; }
    }
}
