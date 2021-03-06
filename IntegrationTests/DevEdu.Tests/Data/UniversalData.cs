using DevEdu.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using DevEdu.Core.Models;
using DevEdu.Tests.Constants;

namespace DevEdu.Tests.Data
{
    public class UniversalData : BaseData
    {
        public static IEnumerable Universal()
        {
            yield return new object[]
            {
                new UserInfoOutPutModel(){ },
                new UserInsertInputModel(){ FirstName = "Хохохо" },
                new List<Role>() { UserRoleData.GetRoleAdmin() },
                MaterialEndpoints.AddTagToMaterialEndpoint
            };
            yield return new object[]
            {
                new CourseInfoOutputModel(){ },
                new CourseInfoFullOutputModel(){ Description = "Test" },
                new List<Role>() { UserRoleData.GetRoleManager() },
                MaterialEndpoints.AddTagToMaterialEndpoint
            };
        }
    }
}