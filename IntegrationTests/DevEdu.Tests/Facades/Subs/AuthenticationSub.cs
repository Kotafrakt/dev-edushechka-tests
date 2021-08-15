using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class AuthenticationSub
    {
        private AuthenticationClient _authentication;

        public AuthenticationSub() { _authentication = new AuthenticationClient(); }

        internal string GetTokenByEmailAndPassword(string email, string password)
        {
            return _authentication.SignInByEmailAndPasswordReturnToken(email, password);
        }
    }
}