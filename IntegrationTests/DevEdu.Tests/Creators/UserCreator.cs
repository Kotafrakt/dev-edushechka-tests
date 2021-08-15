using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class UserCreator : BaseCreator
    {
        public UserInfo RegisterUser<T>(T roles, string token)
        {
            _endPoint = AuthorizationPoints.RegisterPoint;
            var newUser = UserData.GetValidUserInsertInputModelForRegistration(roles);

            var request = _requestHelper.CreatePost(_endPoint, newUser, token);

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