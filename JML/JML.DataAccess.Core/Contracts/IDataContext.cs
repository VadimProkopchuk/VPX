using System.Threading.Tasks;

namespace JML.DataAccess.Core.Contracts
{
    public interface IDataContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
