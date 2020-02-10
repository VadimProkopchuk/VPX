using JML.DataAccess.Context.Extensions;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class StudyGroupConfiguration : IEntityTypeConfiguration<StudyGroup>
    {
        public void Configure(EntityTypeBuilder<StudyGroup> builder)
        {
            builder.ConfigurePrimaryKey().ConfigureAccessAt();
            builder.Property(x => x.Name).IsRequired();

            builder.ToTable("StudyGroups");
        }
    }
}
