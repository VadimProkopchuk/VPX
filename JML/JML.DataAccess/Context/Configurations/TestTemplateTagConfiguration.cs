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
            builder.ConfigurePrimaryKey();
            builder.HasOne(x => x.Tag)
                .WithMany(x => x.TestTemplateTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TestTemplate)
                .WithMany(x => x.TestTemplateTags)
                .HasForeignKey(x => x.TestTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("TestTemplateTags");
        }
    }
}
