using DevEdu.Core.Enums;

namespace DevEdu.Tests.Data
{
    public static class UserRoleData
    {
        public static Role GetRoleAdmin()
        {
            return Role.Admin;
        }

        public static Role GetRoleManager()
        {
            return Role.Manager;
        }

        public static Role GetRoleMethodist()
        {
            return Role.Methodist;
        }

        public static Role GetRoleTeacher()
        {
            return Role.Teacher;
        }

        public static Role GetRoleTutor()
        {
            return Role.Tutor;
        }

        public static Role GetRoleStudent()
        {
            return Role.Student;
        }
    }
}