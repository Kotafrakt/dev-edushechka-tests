using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    internal class MaterialControllerSub
    {
        private MaterialControllerCreator _creator;
        public MaterialControllerSub() { _creator = new MaterialControllerCreator(); }

        internal MaterialInfoWithCoursesOutputModel CreateMaterialInfoWithCourses(string token, List<int> coursesId)
        {
            return _creator.AddMaterialWithCourses(token);
        }
        internal void AddTagToMaterial(string token, int materialId, int tagId)
        {
            _creator.AddTagToMaterial(token, materialId, tagId);
        }
    }
}