using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class TagControllerSub
    {
        private TagControllerCreator _creator;
        public TagControllerSub() { _creator = new TagControllerCreator(); }
        internal TagOutputModel AddTag(string token)
        {
            return _creator.AddTag(token);
        }
    }
}