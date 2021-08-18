using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class AuthenticationControllerFacade
    {
        private readonly AuthenticationCreator _creator;
        private const string _email = "Admin@a.com";
        private const string _password = "12345678";

        public AuthenticationControllerFacade() { _creator = new AuthenticationCreator(); }

        public string SignInByAdmin()
        {
            return GetTokenByEmailAndPassword(email: _email, password: _password);
        }

        public UserInfo SignInByAdminAndRegistrationNewUserByRole<T>(T roles)
        {
            var token = GetTokenByEmailAndPassword(email: _email, password: _password);
            var userInfo = RegisterUser(roles, token);
            userInfo.Token = token;
            return userInfo;
        }

        public UserInfo RegisterNewUserAndSignIn<T>(T roles)
        {
            var token = GetTokenByEmailAndPassword(email: _email, password: _password);
            var userInfo = RegisterUser(roles, token);
            userInfo.Token = GetTokenByEmailAndPassword(userInfo.Email, userInfo.Password);
            return userInfo;
        }

        public string GetTokenByEmailAndPassword(string email, string password)
        {
            return _creator.SignInByEmailAndPasswordReturnToken(email, password);
        }

        public UserInfo RegisterUser<T>(T roles, string token)
        {
            return _creator.RegisterUser(roles, token);
        }
    }
}