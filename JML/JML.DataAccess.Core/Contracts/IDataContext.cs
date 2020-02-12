using JML.Domain.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JML.DataAccess.Core.Contracts
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync();
        DbSet<T> Set<T>() where T : class, IAppEntity<Guid>;
    }
}
