using System;
using System.Collections.Generic;
using JML.Domain.Core.Contracts;

namespace JML.Domain
{
    public class Tag : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<LectureTag> LectureTags { get; set; }
        public virtual ICollection<TestTemplateTag> TestTemplateTags { get; set; }
    }
}
