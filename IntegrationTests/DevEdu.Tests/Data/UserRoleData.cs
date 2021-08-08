using DevEdu.Core.Enums;
using System.Collections;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class UserRoleData
    {
        public static IEnumerable GetRoleAdmin()
        {
            yield return new object[]{ new List<Role> { Role.Admin } };
        }

        public static IEnumerable GetRoleManager()
        {
            yield return new object[] { new List<Role> { Role.Manager } };
        }

        public static IEnumerable GetRoleMethodist()
        {
            yield return new object[] { new List<Role> { Role.Methodist } };
        }

        public static IEnumerable GetRoleTeacher()
        {
            yield return new object[] { new List<Role> { Role.Teacher } };
        }

        public static IEnumerable GetRoleTutor()
        {
            yield return new object[] { new List<Role> { Role.Tutor } };
        }

        public static IEnumerable GetRoleStudent()
        {
            yield return new object[] { new List<Role> { Role.Student } };
        }
    }
}