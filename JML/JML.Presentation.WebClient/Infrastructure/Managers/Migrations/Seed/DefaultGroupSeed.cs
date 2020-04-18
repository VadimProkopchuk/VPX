using JML.DataAccess.Context;
using JML.Domain;
using JML.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace JML.Presentation.WebClient.Infrastructure.Managers.Migrations.Seed
{
    public class DefaultGroupSeed
    {
        public void Seed(AppDbContext context)
        {
            var groups = context.Set<StudyGroup>();
            if (!groups.Any(x => x.Name == "Default Group"))
            {
                groups.AddRange(GetGroupsWithUsers());
                context.SaveChanges();
            }
        }

        private IEnumerable<StudyGroup> GetGroupsWithUsers()
        {
            yield return new StudyGroup
            {
                Name = "Default Group",
                Users = GetStudentGroupUsers().ToList()
            };
        }

        private IEnumerable<User> GetStudentGroupUsers()
        {
            yield return new User()
            {
                Email = "def-student@one.local",
                FirstName = "DefStudent",
                LastName = "One",
                Password = "123",
                UserRoles = new List<UserRole> { new UserRole { Role = Role.Student } }
            };
            yield return new User()
            {
                Email = "def-student@two.local",
                FirstName = "DefStudent",
                LastName = "Two",
                Password = "123",
                UserRoles = new List<UserRole> { new UserRole { Role = Role.Student } }
            };
            yield return new User()
            {
                Email = "def-student@three.local",
                FirstName = "DefStudent",
                LastName = "Three",
                Password = "123",
                UserRoles = new List<UserRole> { new UserRole { Role = Role.Student } }
            };
        }
    }
}
