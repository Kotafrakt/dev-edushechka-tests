using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevEdu.Tests.ControllersTests
{
    public class UniversalControllerTests : BaseControllerTest
    {
        [TestCaseSource(typeof(UniversalData), nameof(UniversalData.Universal))]
        public void Add<T, TU>(TU type, T content, List<Role> roles, string endpoint)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            AuthenticateClient(token);

            _endPoint = endpoint;
            var postData = content;

            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute<T>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = JsonConvert.DeserializeObject<TU>(response.Content);

            ShouldBeEquivalentTo(postData, result);
        }

        public void ShouldBeEquivalentTo<T,TU>(T postData, TU responseData)
        {
            if(postData is UserInsertInputModel data && responseData is UserInfoOutPutModel result)
            {
                data.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.Email));
            }
            
        }
    }
}