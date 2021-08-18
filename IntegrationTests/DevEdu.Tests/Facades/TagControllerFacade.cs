using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class TagControllerFacade
    {
        private TagCreator _creator;

        public TagControllerFacade() { _creator = new TagCreator(); }

        internal TagOutputModel AddTag(string token)
        {
            return _creator.AddTag(token);
        }
    }
}