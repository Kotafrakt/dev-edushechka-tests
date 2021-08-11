using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class UserSub
    {
        private UserCreator _creator;

        public UserSub() { _creator = new UserCreator(); }

        internal UserInfo RegisterUser<T>(T roles)
        {
            return _creator.RegisterUser(roles);
        }
    }
}