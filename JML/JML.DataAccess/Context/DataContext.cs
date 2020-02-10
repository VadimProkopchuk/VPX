using JML.DataAccess.Core.Contracts;
using System.Threading.Tasks;

namespace JML.DataAccess.Context
{
    public class DataContext : IDataContext
    {
        private readonly AppDbContext appDbContext;

        public DataContext(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public int SaveChanges() => appDbContext.SaveChanges();

        public async Task<int> SaveChangesAsync() => await appDbContext.SaveChangesAsync();
    }
}
