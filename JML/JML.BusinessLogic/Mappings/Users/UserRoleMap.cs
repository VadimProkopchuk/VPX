using System;
using JML.ApiModels;
using JML.Domain;
using JML.Domain.Enums;

namespace JML.BusinessLogic.Mappings.Users
{
    internal class UserRoleMap
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
