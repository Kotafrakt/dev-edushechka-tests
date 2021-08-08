using DevEdu.Core.Models;
using DevEdu.Tests.Fillings;

namespace DevEdu.Tests.Facades
{
    internal class TagSub
    {
        private readonly TagFilling _filling;
        public TagSub() { _filling = new TagFilling(); }

        internal TagOutputModel CreateTagCorrect(string token)
        {
            return _filling.CreateTagCorrect(token);
        }
    }
}