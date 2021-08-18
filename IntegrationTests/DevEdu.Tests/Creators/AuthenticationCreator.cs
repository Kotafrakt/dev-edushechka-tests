using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using DevEdu.Tests.Constants;
using DevEdu.Core.Models;

namespace DevEdu.Tests.Creators
{
    public class AuthenticationCreator : BaseCreator
    {
        public string SignInByEmailAndPasswordReturnToken(string email, string password)
        {
            _endPoint = AuthorizationEndpoints.SignInEndpoint;
            var postData = UserData.GetUserSignInputModelByEmailAndPassword(email, password);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData);
            return _client.Execute<string>(request).Data;
        }

        public UserInfo RegisterUser<T>(T roles, string token)
        {
            _endPoint = AuthorizationEndpoints.RegisterEndpoint;
            var newUser = UserData.GetValidUserInsertInputModelForRegistration(roles);

            var request = _requestHelper.CreatePostRequest(_endPoint, newUser, token);

            var responce = _client.Execute<UserFullInfoOutPutModel>(request);
            var user = responce.Data;
            return new()
            {
                Id = user.Id,
                Email = newUser.Email,
                Password = newUser.Password,
                Token = token
            };
        }
    }
}