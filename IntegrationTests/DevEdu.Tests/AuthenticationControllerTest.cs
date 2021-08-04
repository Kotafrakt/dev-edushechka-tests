using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
namespace DevEdu.Tests
{
    public class AuthenticationControllerTest : BaseControllerTest
    {
        [Test]
        public void Register()
        {
            _endPoint = "https://localhost:44386/register";
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
    }
}