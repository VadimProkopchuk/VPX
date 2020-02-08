using System;
using System.Collections.Generic;
using JML.Domain.Base;
using JML.Domain.Templates;

namespace JML.Domain
{
    public class Tag : BaseAuditableEntity<Guid>
    {
        protected Tag() { }

        public Tag(string name) : this()
        {
            Name = name;
        }

        public string Name { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<TestTemplate> Tests { get; set; }
    }
}
