using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class GroupFacade
    {
        private readonly GroupCreator _creator;
        public GroupFacade() { _creator = new GroupCreator(); }
    }
}