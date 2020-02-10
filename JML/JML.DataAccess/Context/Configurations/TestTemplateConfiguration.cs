using JML.DataAccess.Context.Extensions;
using JML.Domain.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class TestTemplateConfiguration : IEntityTypeConfiguration<TestTemplate>
    {
        public void Configure(EntityTypeBuilder<TestTemplate> builder)
        {
            builder.ConfigurePrimaryKey().ConfigureAccessAt();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.CountOfQuestions).IsRequired();
            builder.Property(x => x.ExecuteTime).IsRequired();

            builder.ToTable("TestTemplates");
        }
    }
}
