using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VPX.DataAccess.Core.Contracts;
using VPX.Domain.Core.Contracts;

namespace VPX.DataAccess.Context
{
    public class DataContext : IDataContext
    {
        private readonly AppDbContext appDbContext;

        public DataContext(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Task<int> SaveChangesAsync()
        {
            Timestamps();
            
            return appDbContext.SaveChangesAsync();
        }

        public DbSet<T> Set<T>() where T : class, IAppEntity<Guid>
        {
            return appDbContext.Set<T>();
        }
        private void Timestamps()
        {
            var auditableEntries = appDbContext.ChangeTracker
                .Entries()
                .Where(e => (e.State == EntityState.Modified || e.State == EntityState.Added) &&
                    e.Entity as IAuditableEntity != null);

            foreach (var entry in auditableEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }

                entity.ModifiedAt = DateTime.UtcNow;
            }
        }
    }
}
