using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using DevEdu.Core.Models;
using DevEdu.Core.Enums;
using RestSharp;
using NUnit.Framework;

namespace DevEdu.Tests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string endPoint = "";
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("https://localhost:44386/api");
        }

        [Test]
        public void Register()
        {
            endPoint = "https://localhost:44386/swagger/api/Authentication";
            var postData = new UserInsertInputModel()
            {
                FirstName = "Test",
                LastName = "Test",
                Patronymic = "Test",
                Email = "test@test.com",
                Username = "Tttt",
                Password = "Qweasdewq4312",
                ContractNumber = "qweqdq123",
                City = 1,
                BirthDate = "08.08.2020",
                GitHubAccount = "Git.com",
                Photo = "http://zloo.com",
                PhoneNumber = "9999999",
                Roles = new()
                { 
                    Role.Admin,
                    Role.Manager,
                    Role.Methodist,
                    Role.Teacher,
                    Role.Tutor,
                    Role.Student
                }
            };

            var jsonData = JsonConvert.SerializeObject(postData);
            headers.Add("content-type", "application/json");
            var result = _request.Post(endPoint, headers, jsonData);

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public void SignIn()
        {            
        }

        [Test]
        public void Can_Retrieve_All_Students()
        {
            endPoint = @"Data Source=80.78.240.16;Initial Catalog = DevEdu;Persist Security Info=True;User ID = student;Password=qwe!23;";
            headers.Add("content-type", "application/json");
            var result = _request.Get(endPoint, headers);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);            
        }
    }
}