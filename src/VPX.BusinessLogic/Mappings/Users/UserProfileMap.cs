using System.Linq;
using VPX.ApiModels;
using VPX.Domain;
using VPX.Enums;

namespace VPX.BusinessLogic.Mappings.Users
{
    internal class UserProfileMap
    {
        public static UserProfileModel Map(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserProfileModel
            {
                ActiveAt = user.ActiveAt,
                CreatedAt = user.CreatedAt,
                FullName = user.FirstName + " " + user.LastName,
                GroupName = user.Group?.Name,
                IsLocked = user.IsLocked,
                Roles = user.UserRoles.Select(x => UserRoleMap.Map(x.Role)).ToList(),
                HasStudentRole = user.UserRoles.Any(x => x.Role == Role.Student),
                HasTeacherRole = user.UserRoles.Any(x => x.Role == Role.Teacher),
                Tests = user.Tests
                    .Select(x => new TestNameModel
                    {
                        Name = x.TestTemplate.Name,
                        Id = x.TestTemplateId
                    })
                    .GroupBy(x => x.Id)
                    .Select(x => x.First())
                    .ToList(),
                Image = user.AvatarBase64
            };
        }
    }
}
