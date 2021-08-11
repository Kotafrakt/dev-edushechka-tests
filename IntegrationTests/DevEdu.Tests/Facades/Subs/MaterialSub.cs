using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    internal class MaterialSub
    {
        private MaterialCreator _creator;
        public MaterialSub() { _creator = new MaterialCreator(); }

        internal MaterialInfoWithCoursesOutputModel CreateMaterialInfoWithCourses(string token, List<int> coursesId)
        {
            return new(); //_creator.CreateMaterialCorrect(token);
        }
        internal void AddTagToMaterial(string token, int materialId, int tagId)
        {
            //_creator.AddTagToMaterial(token, materialId, tagId);
        }
    }
}