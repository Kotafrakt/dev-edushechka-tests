using DevEdu.Core.Models;
using System;

namespace DevEdu.Tests.Data
{
    public class MaterialData : BaseData
    {
        public static MaterialInputModel GetInvalidMaterialInputModel()
        {
            return new()
            {
                Content = "zlo materials"
            };
        }
    }
}