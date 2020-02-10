using JML.Domain.Core.Contracts;
using System;
using System.Linq;

namespace JML.DataAccess.Core.Contracts
{
    public interface IAppEntityRepository<T> where T : IAppEntity<Guid>
    {
        IQueryable<T> GetQuery();
        T Add(T entity);
        T Update(T entity);
        T Remove(T entity);
        T Remove(Guid id);
    }
}
