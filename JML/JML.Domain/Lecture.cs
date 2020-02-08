using System;
using System.Collections.Generic;
using JML.Domain.Base;

namespace JML.Domain
{
    public class Lecture : BaseAuditableEntity<Guid>
    {
        protected Lecture() { }

        public Lecture(string name, string url, string file, TimeSpan timeToRead) 
            : this()
        {
            Name = name;
            Url = url;
            File = file;
            TimeToRead = timeToRead;
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string File { get; set; }
        public TimeSpan TimeToRead { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
