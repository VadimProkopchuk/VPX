using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPX.DataAccess.Context.Extensions;
using VPX.Domain;

namespace VPX.DataAccess.Context.Configurations
{
    public class StudyGroupConfiguration : IEntityTypeConfiguration<StudyGroup>
    {
        public void Configure(EntityTypeBuilder<StudyGroup> builder)
        {
            builder.ConfigurePrimaryKey();
            builder.Property(x => x.Name).IsRequired();

            builder.ToTable("StudyGroups");
        }
    }
}
