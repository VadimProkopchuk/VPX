namespace VPX.BusinessLogic.Constants
{
    public static class AppRoles
    {
        public const string Student = "Student";
        public const string Teacher = "Teacher";
        public const string Admin = "Admin";

        public const string TeacherOrAdmin = Teacher + "," + Admin;
    }
}
