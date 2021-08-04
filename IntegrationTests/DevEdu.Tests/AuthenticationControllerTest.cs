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
        readonly Dictionary<string, string> _headers = new();
        string _endPoint = "";
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("https://localhost:44386/api");
        }

        [Test]
        public void Register()
        {
            _endPoint = "https://localhost:44386/swagger/api/Authentication";
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
            _headers.Add("content-type", "application/json");
            var result = _request.Post(_endPoint, _headers, jsonData);

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public void SignIn()
        {            
        }

        [Test]
        public void Can_Retrieve_All_Students()
        {
            _endPoint = @"Data Source=80.78.240.16;Initial Catalog = DevEdu;Persist Security Info=True;User ID = student;Password=qwe!23;";
            _headers.Add("content-type", "application/json");
            var result = _request.Get(_endPoint, _headers);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);            
        }
    }
}