using System;
using VPX.Domain.Core.Contracts;

namespace VPX.Domain
{
    public class LectureTag : IAppEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; protected set; }
        public Guid TagId { get; set; }
        public Guid LectureId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Lecture Lecture { get; set; }
    }
}
