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
    }
}
