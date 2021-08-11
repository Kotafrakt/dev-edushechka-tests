using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class MaterialSub
    {
        private MaterialCreator _creator;
        public MaterialSub() { _creator = new MaterialCreator(); }

        internal MaterialInfoOutputModel CreateMaterialCorrect(string token)
        {
            return _creator.CreateMaterialCorrect(token);
        }
    }
}