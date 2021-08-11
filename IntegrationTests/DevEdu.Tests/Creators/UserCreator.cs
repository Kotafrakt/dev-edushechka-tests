using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.Creators
{
    public class UserCreator : BaseCreator
    {
        public UserSignInputModel RegisterCorrectUser(List<Role> roles)
        {
            _endPoint = RegisterPoint;
            var postData = UserData.GetUserInsertInputModelForRegistration_Correct(roles);

            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var responce = _client.Execute<UserFullInfoOutPutModel>(request);
            var result = responce.Data;

            //postData.Should().BeEquivalentTo(result, options => options
            //        .Excluding(obj => obj.ExileDate)
            //        .Excluding(obj => obj.Id)
            //        .Excluding(obj => obj.RegistrationDate));

            return new()
            {
                Email = postData.Email,
                Password = postData.Password
            };
        }
    }
}