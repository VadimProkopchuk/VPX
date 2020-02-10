using JML.DataAccess.Context.Extensions;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class TestTemplateTagConfiguration : IEntityTypeConfiguration<TestTemplateTag>
    {
        public void Configure(EntityTypeBuilder<TestTemplateTag> builder)
        {
            builder.ConfigurePrimaryKey().ConfigureAccessAt();
            builder.HasOne(x => x.Tag).WithMany(x => x.TestTemplateTags).HasForeignKey(x => x.TagId);
            builder.HasOne(x => x.TestTemplate).WithMany(x => x.TestTemplateTags).HasForeignKey(x => x.TestTemplateId);
        }
    }
}
