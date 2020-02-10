using JML.DataAccess.Context.Extensions;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JML.DataAccess.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ConfigurePrimaryKey().ConfigureAccessAt();
            builder.HasOne(x => x.Group)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.CountOfInvalidAttempts).IsRequired();
            builder.Property(x => x.IsLocked).IsRequired();

            builder.ToTable("Users");
        }
    }
}
