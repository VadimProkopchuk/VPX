using System.Collections.Generic;
using JML.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using JML.Domain;
using System.Threading.Tasks;
using JML.Domain.Enums;

namespace JML.Presentation.ConsolePresentation
{
    class Program
    {
        static async Task Main()
        {
            var connectionOptions = new DbContextOptionsBuilder<AppDbContext>()
                // .UseSqlServer("Server=.;Database=JML.Storage.Dev;Trusted_Connection=True;")
                .UseSqlServer("Server=V-PROKOPCHUK\\VPROKOPCHUK;Database=JML.Storage.Dev;User Id=JML_USER;Password=!QAZ2wsx12")
                .UseLazyLoadingProxies()
                .Options;
            await using var appDbContext = new AppDbContext(connectionOptions);
            var dataContext = new DataContext(appDbContext);

            var user = new User
            {
                FirstName = "Vadim",
                LastName = "Student",
                Email = "vadim@student.local",
                Password = "123",
                UserRoles = new List<UserRole>()
                {
                    new UserRole
                    {
                        Role = Role.Student,

                    }
                }
            };

            appDbContext.Set<User>().Add(user);
            await dataContext.SaveChangesAsync();
        }
    }
}
