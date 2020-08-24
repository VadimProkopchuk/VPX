using System;
using VPX.ApiModels;
using VPX.Domain;
using VPX.Enums;

namespace VPX.BusinessLogic.Mappings.Users
{
    public class UserRoleMap
    {
        public static RoleModel Map(UserRole userRole)
        {
            if (userRole == null)
            {
                return null;
            }

            return new RoleModel()
            {
                Value = userRole.Role.ToString(),
                Display = Map(userRole.Role)
            };
        }

        public static string Map(Role role)
        {
            switch (role)
            {
                case Role.Student: return "Студент";
                case Role.Teacher: return "Преподаватель";
                case Role.Admin: return "Администратор";
            }

            return String.Empty;
        }
    }
}
