using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class UserControllerSub
    {
        private UserControllerCreator _creator;
        public UserControllerSub() { _creator = new UserControllerCreator(); }
    }
}