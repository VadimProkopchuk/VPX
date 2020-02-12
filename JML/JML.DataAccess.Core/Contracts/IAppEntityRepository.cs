using JML.Domain.Core.Contracts;
using System;
using System.Linq;

namespace JML.DataAccess.Core.Contracts
{
    public interface IAppEntityRepository<T> where T : IAppEntity<Guid>
    {
        IQueryable<T> GetQuery();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(Guid id);
    }
}
