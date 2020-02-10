using JML.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using JML.Domain;
using System.Threading.Tasks;

namespace JML.Presentation.ConsolePresentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=.;Database=JML.Storage.Dev;Trusted_Connection=True;")
                .Options;
            var appDbContext = new AppDbContext(connectionOptions);
            var dataContext = new DataContext(appDbContext);

            appDbContext.Set<Tag>().Add(new Tag() { Name = "Test" });

            await dataContext.SaveChangesAsync();



            Console.WriteLine("Hello World!");
        }


    }
}
