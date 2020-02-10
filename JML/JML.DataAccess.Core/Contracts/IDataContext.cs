using System.Threading.Tasks;

namespace JML.DataAccess.Core.Contracts
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync();
    }
}
