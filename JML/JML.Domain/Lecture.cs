using System;
using System.Collections.Generic;
using JML.Domain.Core.Contracts;

namespace JML.Domain
{
    public class Lecture : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public TimeSpan TimeToRead { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<LectureTag> LectureTags { get; set; }
    }
}
