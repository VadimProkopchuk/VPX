using JML.DataAccess.Context.Extensions;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class LectureTagConfiguration : IEntityTypeConfiguration<LectureTag>
    {
        public void Configure(EntityTypeBuilder<LectureTag> builder)
        {
            builder.ConfigurePrimaryKey().ConfigureAccessAt();
            builder.HasOne(x => x.Tag).WithMany(x => x.LectureTags).HasForeignKey(x => x.TagId);
            builder.HasOne(x => x.Lecture).WithMany(x => x.LectureTags).HasForeignKey(x => x.LectureId);
        }
    }
}
