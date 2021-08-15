using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class AuthenticationControllerSub
    {
        private AuthenticationControllerCreator _creator;

        public AuthenticationControllerSub() { _creator = new AuthenticationControllerCreator(); }

        internal string GetTokenByEmailAndPassword(string email, string password)
        {
            return _creator.SignInByEmailAndPasswordReturnToken(email, password);
        }

        internal UserInfo RegisterUser<T>(T roles, string token)
        {
            return _creator.RegisterUser(roles, token);
        }
    }
}