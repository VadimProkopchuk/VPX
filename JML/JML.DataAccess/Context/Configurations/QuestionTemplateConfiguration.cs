using JML.DataAccess.Context.Extensions;
using JML.Domain.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class QuestionTemplateConfiguration : IEntityTypeConfiguration<QuestionTemplate>
    {
        public void Configure(EntityTypeBuilder<QuestionTemplate> builder)
        {
            builder.ConfigurePrimaryKey().ConfigureAccessAt();
            builder.HasOne(x => x.Test).WithMany(x => x.Questions).HasForeignKey(x => x.TestTemplateId);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ControlType).IsRequired();
        }
    }
}
