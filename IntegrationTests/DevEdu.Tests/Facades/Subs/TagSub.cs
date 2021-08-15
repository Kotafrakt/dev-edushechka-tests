using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class TagSub
    {
        private readonly TagControllerCreator _creator;
        public TagSub() { _creator = new TagControllerCreator(); }

        internal TagOutputModel AddTag(string token)
        {
            return _creator.AddTag(token);
        }
    }
}