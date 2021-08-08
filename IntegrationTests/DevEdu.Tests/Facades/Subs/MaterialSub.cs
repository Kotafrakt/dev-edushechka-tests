using DevEdu.Core.Models;
using DevEdu.Core.Models.Material;
using DevEdu.Tests.Fillings;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    internal class MaterialSub
    {
        private MaterialFilling _filling;
        public MaterialSub() { _filling = new MaterialFilling(); }

        internal MaterialInfoWithCoursesOutputModel CreateMaterialInfoWithCoursesCorrect(string token, List<int> coursesId)
        {
            return _filling.CreateMaterialInfoWithCoursesCorrect(token, coursesId);
        }
    }
}