using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPX.DataAccess.Context.Extensions;
using VPX.Domain.Templates;

namespace VPX.DataAccess.Context.Configurations
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
