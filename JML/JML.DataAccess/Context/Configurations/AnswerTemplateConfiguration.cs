using JML.DataAccess.Context.Extensions;
using JML.Domain.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class AnswerTemplateConfiguration : IEntityTypeConfiguration<AnswerTemplate>
    {
        public void Configure(EntityTypeBuilder<AnswerTemplate> builder)
        {
            builder.ConfigurePrimaryKey();
            builder.HasOne(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.QuestionTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Answer).IsRequired();
            builder.Property(x => x.IsCorrect).IsRequired();

            builder.ToTable("AnswerTemplates");
        }
    }
}
