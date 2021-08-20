using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class TagFacade
    {
        private readonly TagCreator _creator;
        public TagFacade() { _creator = new TagCreator(); }

        internal TagOutputModel AddTag(string token)
        {
            return _creator.AddTag(token);
        }
    }
}