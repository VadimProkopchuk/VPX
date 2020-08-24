using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPX.DataAccess.Context.Extensions;
using VPX.Domain;

namespace VPX.DataAccess.Context.Configurations
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
