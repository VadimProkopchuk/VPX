using System.Collections.Generic;
using System.Linq;
using JML.DataAccess.Context;
using JML.Domain;
using JML.Domain.Enums;

namespace JML.Presentation.WebClient.Infrastructure.Managers.Migrations.Seed
{
    public class DefaultAdminSeed
    {
        public void Seed(AppDbContext context)
        {
            var users = context.Set<User>();

            if (!users.Any())
            {
                users.AddRange(GetUsers());
                context.SaveChanges();
            }
        }

        private IEnumerable<User> GetUsers()
        {
            yield return new User()
            {
                Email = "vadim@admin.local",
                FirstName = "Vadim",
                LastName = "Prokopchuk",
                Password = "123",
                UserRoles = new List<UserRole> { new UserRole { Role = Role.Admin } }
            };
        }
    }
}
