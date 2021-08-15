using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class GroupControllerSub
    {
        private readonly GroupControllerCreator _creator;
        public GroupControllerSub() { _creator = new GroupControllerCreator(); }
    }
}