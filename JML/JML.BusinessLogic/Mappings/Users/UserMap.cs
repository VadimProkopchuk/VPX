using System.Collections.Generic;
using System.Linq;
using JML.ApiModels;
using JML.Domain;

namespace JML.BusinessLogic.Mappings.Users
{
    internal class UserMap
    {
        public static UserModel Map(User user)
        {
            if (user == null)
            {
                return null;
            }

            var userRoles = user.UserRoles ?? new List<UserRole>();
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                GroupName = user.Group?.Name,
                Email = user.Email,
                Roles = userRoles.Select(UserRoleMap.Map).ToList()
            };
        }
    }
}
