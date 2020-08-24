using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPX.DataAccess.Context.Extensions;
using VPX.Domain;

namespace VPX.DataAccess.Context.Configurations
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
