using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPX.DataAccess.Context.Extensions;
using VPX.Domain;

namespace VPX.DataAccess.Context.Configurations
{
    public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.ConfigurePrimaryKey();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Url).IsRequired();
            builder.HasIndex(x => x.Url).IsUnique();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.TimeToRead).IsRequired();

            builder.ToTable("Lectures");
        }
    }
}
