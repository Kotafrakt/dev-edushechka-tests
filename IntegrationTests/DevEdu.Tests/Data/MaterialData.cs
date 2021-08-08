using DevEdu.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class MaterialData : BaseData
    {
        public static MaterialInputModel GetMaterialInputModel_Correct()
        {
            return new()
            {
                Content = "zlo materials"
            };
        }

        public static MaterialWithGroupsInputModel GetMaterialWithGroupsInputModel_Correct(List<int> groupsId)
        {
            return new()
            {
                GroupsIds = groupsId,
                Content = "Kot materials",
                TagsIds = null
            };
        }

        public static MaterialWithCoursesInputModel GetMaterialWithCoursesInputModel_Correct(List<int> coursesId)
        {
            return new()
            {
                CoursesIds = coursesId,
                Content = "Kot materials",
                TagsIds = null
            };
        }

        public static IEnumerable СheckByAllRoles()
        {
            yield return new object[] { UserRoleData.GetRoleAdmin() };
            yield return new object[] { UserRoleData.GetRoleManager() };
            yield return new object[] { UserRoleData.GetRoleMethodist() };
            yield return new object[] { UserRoleData.GetRoleTeacher() };
            yield return new object[] { UserRoleData.GetRoleTutor() };
            yield return new object[] { UserRoleData.GetRoleStudent() };
            yield return new object[] { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleMethodist() };
            yield return new object[] { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleTutor() };
        }
        public static IEnumerable СheckByRolesTeacherAndTutor()
        {
            yield return new object[] { UserRoleData.GetRoleAdmin() };
            yield return new object[] { UserRoleData.GetRoleTeacher() };
            yield return new object[] { UserRoleData.GetRoleTutor() };
            yield return new object[] { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleTutor() };
        }
        
        public static IEnumerable СheckByRolesTeacherAndMethodist()
        {
            yield return new object[] { UserRoleData.GetRoleAdmin() };
            yield return new object[] { UserRoleData.GetRoleTeacher() };
            yield return new object[] { UserRoleData.GetRoleMethodist() };
            yield return new object[] { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleMethodist() };
        }

        public static IEnumerable СheckByRolesMethodist()
        {
            yield return new object[] { UserRoleData.GetRoleAdmin() };
            yield return new object[] { UserRoleData.GetRoleMethodist() };
        }

        public static IEnumerable СheckByAllRolesButManager()
        {
            yield return new object[] { UserRoleData.GetRoleAdmin() };
            yield return new object[] { UserRoleData.GetRoleMethodist() };
            yield return new object[] { UserRoleData.GetRoleTeacher() };
            yield return new object[] { UserRoleData.GetRoleTutor() };
            yield return new object[] { UserRoleData.GetRoleStudent() };
            yield return new object[] { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleMethodist() };
            yield return new object[] { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleTutor() };
        }
    }
}