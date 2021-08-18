using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class GroupControllerFacade
    {
        private readonly GroupCreator _creator;
        public GroupControllerFacade() { _creator = new GroupCreator(); }
    }
}