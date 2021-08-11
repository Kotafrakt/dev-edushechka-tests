using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class MaterialData : BaseData
    {
        public static MaterialInputModel GetValidMaterialInputModel()
        {
            return new()
            {

                Content = "zlo materials"
            };
        }

        public static MaterialInputModel GetUpdateMaterialInputModel()
        {
            return new()
            {

                Content = "Kot materials"
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

        public static MaterialWithCoursesInputModel GetMaterialWithCoursesInputModelForFillingDB(List<int> coursesId)
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
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleAdmin() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleManager() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleMethodist() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTutor() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleStudent() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleMethodist() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleTutor() }};
        }

        public static IEnumerable СheckByRolesTeacherAndTutor()
        {
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleAdmin() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTutor() } };
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleTutor() }};
        }
        
        public static IEnumerable СheckByRolesTeacherAndMethodist()
        {
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleAdmin() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleMethodist() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleMethodist() }};
        }

        public static IEnumerable СheckByRolesMethodist()
        {
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleAdmin() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleMethodist() }};
        }

        public static IEnumerable СheckByAllRolesButManager()
        {
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleAdmin() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleMethodist() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTutor() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleStudent() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleMethodist() }};
            yield return new object[] { new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleTutor() }};
        }
    }
}