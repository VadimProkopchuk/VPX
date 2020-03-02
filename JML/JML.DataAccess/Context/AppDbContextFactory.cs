using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JML.DataAccess.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=JML.Storage.Dev;Trusted_Connection=True;");
            // optionsBuilder.UseSqlServer("Server=V-PROKOPCHUK\\VPROKOPCHUK;Database=JML.Storage.Dev;User Id=JML_USER;Password=!QAZ2wsx12");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}