using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class AuthenticationSub
    {
        private AuthenticationControllerCreator _authentication;

        public AuthenticationSub() { _authentication = new AuthenticationControllerCreator(); }

        internal string GetTokenByEmailAndPassword(string email, string password)
        {
            return _authentication.SignInByEmailAndPasswordReturnToken(email, password);
        }
    }
}