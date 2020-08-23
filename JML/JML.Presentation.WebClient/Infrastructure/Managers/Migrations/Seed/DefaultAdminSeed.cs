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
                users.AddRange(GetAdmins());
                context.SaveChanges();
            }
        }

        private IEnumerable<User> GetAdmins()
        {
            yield return new User()
            {
                Email = "julia.bogomolowa@gmail.com",
                FirstName = "Юля",
                LastName = "Боголомова",
                Password = "!QAZ2wsx12",
                UserRoles = new List<UserRole> { new UserRole { Role = Role.Admin } }
            };
        }
    }
}
