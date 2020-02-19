using JML.Domain.Enums;
using System;

namespace JML.Presentation.WebClient.Infrastructure.Presenters
{
    public static class RolePresenter
    {
        public static string Present(this Role role)
        {
            switch (role)
            {
                case Role.Student: return "Ученик/Студент";
                case Role.Teacher: return "Учитель/Преподаватель";
                case Role.Admin: return "Администратор";
            }

            return String.Empty;
        }
    }
}
