using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    public class MaterialFacade
    {
        private MaterialCreator _creator;
        public MaterialFacade() { _creator = new MaterialCreator(); }

        public MaterialInfoWithCoursesOutputModel CreateMaterialInfoWithCourses(string token, List<int> coursesId)
        {
            return _creator.AddMaterialWithCourses(token);
        }
        public void AddTagToMaterial(string token, int materialId, int tagId)
        {
            _creator.AddTagToMaterial(token, materialId, tagId);
        }
    }
}