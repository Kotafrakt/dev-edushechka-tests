using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using System.Collections.Generic;

namespace DevEdu.Tests.Creators
{
    public class UserCreator : BaseCreator
    {
        public UserSignInputModel RegisterCorrectUser(List<Role> roles)
        {
            _endPoint = AuthorizationPoints.RegisterPoint;
            var postData = UserData.GetInvalidUserInsertInputModelForRegistration(roles);

            var request = _requestHelper.Post(_endPoint, postData);

            _client.Execute<UserFullInfoOutPutModel>(request);
            return new()
            {
                Email = postData.Email,
                Password = postData.Password
            };
        }
    }
}