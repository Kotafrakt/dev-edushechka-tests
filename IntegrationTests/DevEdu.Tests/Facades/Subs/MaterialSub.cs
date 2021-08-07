using DevEdu.Core.Models;
using DevEdu.Tests.Fillings;

namespace DevEdu.Tests.Facades
{
    internal class MaterialSub
    {
        private MaterialFilling _filling;
        public MaterialSub() { _filling = new MaterialFilling(); }

        internal MaterialInfoOutputModel CreateMaterialCorrect(string token)
        {
            return _filling.CreateMaterialCorrect(token);
        }
    }
}