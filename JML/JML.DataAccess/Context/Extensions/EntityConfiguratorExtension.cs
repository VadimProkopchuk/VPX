using JML.Domain.Core.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace JML.DataAccess.Context.Extensions
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

        public static EntityTypeBuilder<T> ConfigureAccessAt<T>(this EntityTypeBuilder<T> modelBuilder)
            where T : class, IAuditableEntity
        {
            modelBuilder
                .Property(x => x.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder
                .Property(x => x.ModifiedAt)
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();

            return modelBuilder;
        }
    }
}
