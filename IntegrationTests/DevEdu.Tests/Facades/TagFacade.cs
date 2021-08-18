using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class TagFacade
    {
        private TagCreator _creator;
        public TagFacade() { _creator = new TagCreator(); }
    }
}