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

            var request = _requestHelper.Post(_endPoint, newUser);
            request = _requestHelper.Autorize(request, token);

            _client.Execute<UserFullInfoOutPutModel>(request);
            return new()
            {
                Email = newUser.Email,
                Password = newUser.Password,
                Token = token
            };
        }
    }
}