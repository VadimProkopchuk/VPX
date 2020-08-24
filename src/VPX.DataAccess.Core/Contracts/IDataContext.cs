using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VPX.Domain.Core.Contracts;

namespace VPX.DataAccess.Core.Contracts
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync();
        DbSet<T> Set<T>() where T : class, IAppEntity<Guid>;
    }
}
