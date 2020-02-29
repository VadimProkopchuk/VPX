using JML.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using JML.Domain;
using System.Threading.Tasks;

namespace JML.Presentation.ConsolePresentation
{
    class Program
    {
        static async Task Main()
        {
            var connectionOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=.;Database=JML.Storage.Dev;Trusted_Connection=True;")
                .UseLazyLoadingProxies()
                .Options;
            await using var appDbContext = new AppDbContext(connectionOptions);
            var dataContext = new DataContext(appDbContext);

            var users = await appDbContext.Set<User>().ToListAsync();

            foreach (var user in users)
            {
                user.UserRoles.Add(new UserRole()
                {
                    Role = Domain.Enums.Role.Admin
                });
            }

            await dataContext.SaveChangesAsync();

        }
    }
}
