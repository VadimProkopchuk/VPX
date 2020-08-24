using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPX.Domain.Core.Contracts;

namespace VPX.DataAccess.Context.Extensions
{
    public static class EntityConfiguratorExtension
    {
        public static EntityTypeBuilder<T> ConfigurePrimaryKey<T>(this EntityTypeBuilder<T> modelBuilder)
            where T : class, IAppEntity<Guid>
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            return modelBuilder;
        }
    }
}
