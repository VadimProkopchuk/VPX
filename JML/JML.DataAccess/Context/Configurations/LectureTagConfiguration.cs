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
            builder.ConfigurePrimaryKey();
            builder.HasOne(x => x.Tag)
                .WithMany(x => x.LectureTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Lecture)
                .WithMany(x => x.LectureTags)
                .HasForeignKey(x => x.LectureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("LectureTags");
        }
    }
}
