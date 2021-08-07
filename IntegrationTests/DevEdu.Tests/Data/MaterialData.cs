using DevEdu.Core.Models;
using System;

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
    }
}