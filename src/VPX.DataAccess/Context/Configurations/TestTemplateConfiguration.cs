using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPX.DataAccess.Context.Extensions;
using VPX.Domain.Templates;

namespace VPX.DataAccess.Context.Configurations
{
    public class TestTemplateConfiguration : IEntityTypeConfiguration<TestTemplate>
    {
        public void Configure(EntityTypeBuilder<TestTemplate> builder)
        {
            builder.ConfigurePrimaryKey();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.CountOfQuestions).IsRequired();
            builder.Property(x => x.ExecuteTime).IsRequired();

            builder.ToTable("TestTemplates");
        }
    }
}
