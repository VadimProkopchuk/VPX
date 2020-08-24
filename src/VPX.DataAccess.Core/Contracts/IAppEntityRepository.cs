using System;
using System.Linq;
using VPX.Domain.Core.Contracts;

namespace VPX.DataAccess.Core.Contracts
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
