using System;
using System.Collections.Generic;
using VPX.Domain.Core.Contracts;

namespace VPX.Domain
{
    public class Tag : IAppEntity<Guid>, IAuditableEntity
    {
        public Tag() { }

        public Tag(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; protected set; }

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<LectureTag> LectureTags { get; set; }
        public virtual ICollection<TestTemplateTag> TestTemplateTags { get; set; }
    }
}
