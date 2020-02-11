using JML.DataAccess.Core.Contracts;
using JML.Domain.Core.Contracts;
using JML.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JML.DataAccess.Repository
{
    public class AppEntityRepository<T> : IAppEntityRepository<T> where T : class, IAppEntity<Guid>
    {
        public AppEntityRepository(IDataContext dataContext)
        {
            EntitiesSet = dataContext.Set<T>();
        }

        protected DbSet<T> EntitiesSet { get; private set; }

        public void Add(T entity)
        {
            entity.NotNull();
            EntitiesSet.Attach(entity);
            EntitiesSet.Add(entity);
        }

        public IQueryable<T> GetQuery() => EntitiesSet;

        public void Remove(T entity)
        {
            entity.NotNull();
            EntitiesSet.Attach(entity);
            EntitiesSet.Remove(entity);
        }

        public void Remove(Guid id)
        {
            var entity = EntitiesSet.FirstOrDefault(x => x.Id == id);
            Remove(entity);
        }

        public void Update(T entity)
        {
            entity.NotNull();
            EntitiesSet.Attach(entity);
            EntitiesSet.Update(entity);
        }
    }
}
