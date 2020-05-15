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
            builder.HasOne(x => x.QuestionTemplate)
                .WithMany()
                .HasForeignKey(x => x.QuestionTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("KnowledgeTestQuestions");
        }
    }
}
