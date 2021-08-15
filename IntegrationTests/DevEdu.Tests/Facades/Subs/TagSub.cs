using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class TagSub
    {
        private readonly TagCreator _creator;
        public TagSub() { _creator = new TagCreator(); }

        internal TagOutputModel CreateTagCorrect(string token)
        {
            return _creator.CreateTagCorrect(token);
        }
    }
}