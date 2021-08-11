using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class MaterialSub
    {
        private MaterialCreator _creator;
        public MaterialSub() { _creator = new MaterialCreator(); }

        internal MaterialInfoWithCoursesOutputModel CreateMaterialInfoWithCourses(string token, List<int> coursesId)
        {
            return _creator.CreateMaterialCorrect(token);
        }
        internal void AddTagToMaterial(string token, int materialId, int tagId)
        {
            _filling.AddTagToMaterial(token, materialId, tagId);
        }
    }
}