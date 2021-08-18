using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class UserFacade
    {
        private UserCreator _creator;
        public UserFacade() { _creator = new UserCreator(); }
    }
}