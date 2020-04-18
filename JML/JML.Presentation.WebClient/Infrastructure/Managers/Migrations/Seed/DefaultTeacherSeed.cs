using JML.DataAccess.Context;
using JML.Domain;
using JML.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace JML.Presentation.WebClient.Infrastructure.Managers.Migrations.Seed
{
    public class DefaultTeacherSeed
    {
        public void Seed(AppDbContext context)
        {
            var users = context.Set<User>();
            if (!users.Any(x => x.Email == "def-teacher@one.local"))
            {
                users.AddRange(GetTeachers());
                context.SaveChanges();
            }
        }

        private IEnumerable<User> GetTeachers()
        {
            yield return new User()
            {
                Email = "def-teacher@one.local",
                FirstName = "DefTeacher",
                LastName = "One",
                Password = "123",
                UserRoles = new List<UserRole> { new UserRole { Role = Role.Teacher} }
            };
        }
    }
}
