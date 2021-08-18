using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class UserControllerFacade
    {
        private UserCreator _creator;
        public UserControllerFacade() { _creator = new UserCreator(); }
    }
}