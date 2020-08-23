using JML.DataAccess.Context.Extensions;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class LiteratureConfiguration : IEntityTypeConfiguration<Literature>
    {
        public void Configure(EntityTypeBuilder<Literature> builder)
        {
            builder.ConfigurePrimaryKey();
            builder.ToTable("Literature");
        }
    }
}
