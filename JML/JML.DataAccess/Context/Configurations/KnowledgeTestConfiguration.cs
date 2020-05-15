using JML.DataAccess.Context.Extensions;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class KnowledgeTestConfiguration : IEntityTypeConfiguration<KnowledgeTest>
    {
        public void Configure(EntityTypeBuilder<KnowledgeTest> builder)
        {
            builder.ConfigurePrimaryKey();
            builder.HasOne(x => x.TestTemplate)
                .WithMany()
                .HasForeignKey(x => x.TestTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User)
                .WithMany(x => x.Tests)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.ExpiredAt).IsRequired();
            
            builder.ToTable("KnowledgeTests");
        }
    }
}
