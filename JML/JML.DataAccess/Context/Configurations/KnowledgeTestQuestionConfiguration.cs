using JML.DataAccess.Context.Extensions;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class KnowledgeTestQuestionConfiguration : IEntityTypeConfiguration<KnowledgeTestQuestion>
    {
        public void Configure(EntityTypeBuilder<KnowledgeTestQuestion> builder)
        {
            builder.ConfigurePrimaryKey();
            builder.HasOne(x => x.KnowledgeTest)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.KnowledgeTestId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.QuestionTemplate)
                .WithMany()
                .HasForeignKey(x => x.QuestionTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SelectedAnswer)
                .WithMany()
                .HasForeignKey(x => x.SelectedAnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("KnowledgeTestQuestions");
        }
    }
}
