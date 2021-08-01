using DevEdu.Tests.Requests;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using DevEdu.Tests.Models;
using DevEdu.Tests.Enums;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace DevEdu.Tests
{
    [TestClass]
    public class AuthenticationControllerTest : BaseControllerTest
    {
        public AuthenticationControllerTest(IRequest request) : base(request) { }
        
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string endPoint = "";
        private RestClient _client;

        [TestInitialize()]
        public virtual void TestInitialize()
        {
            _client = new RestClient("http://localhost:24144");
            _client.CookieContainer = new System.Net.CookieContainer();
        }


        [TestCategory("POST")]
        [TestMethod]
        public async Task Register()
        {
            endPoint = @"Data Source=80.78.240.16;Initial Catalog = DevEdu;Persist Security Info=True;User ID = student;Password=qwe!23;";
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
            var result = await _request.PostAsync(endPoint, headers, jsonData);

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestCategory("POST")]
        [TestMethod]
        public async Task SignIn()
        {
            
        }

        [TestCategory("GET")]
        [TestMethod]
        public async Task Can_Retrieve_All_Students()
        {
            endPoint = @"Data Source=80.78.240.16;Initial Catalog = DevEdu;Persist Security Info=True;User ID = student;Password=qwe!23;";
            headers.Add("content-type", "application/json");
            var result = await _request.GetAsync(endPoint, headers);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);            
        }
    }
}