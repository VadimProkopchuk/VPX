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
            using var appDbContext = new AppDbContext(connectionOptions);
            var dataContext = new DataContext(appDbContext);

            var groups = await appDbContext.Set<StudyGroup>().ToListAsync();
            var tags = await appDbContext.Set<Tag>().ToListAsync();

            foreach (var tag in tags)
            {
                Console.WriteLine(tag.TestTemplateTags.Count);
            }

            await dataContext.SaveChangesAsync();

        }


    }
}
