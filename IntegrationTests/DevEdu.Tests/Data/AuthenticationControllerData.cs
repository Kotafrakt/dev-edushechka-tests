using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using System;

namespace DevEdu.Tests
{
    public class AuthenticationControllerData
    {
        public const string RegisterEndPoint = "register";
        public const string SignInEndPoint = "sign-in";
        private const string _dateFormat = "MM/dd/yyyy hh:mm:ss.fff tt";

        public static UserInsertInputModel GetUserInsertInputModelForRegistration_1()
        {
            return new()
            {
                FirstName = "Test",
                LastName = "Test",
                Patronymic = "Test",
                Email = "a@a.ru",
                Username = "Tttt",
                Password = "12345678",
                ContractNumber = string.Format(Convert.ToString(DateTime.Now.ToString(_dateFormat))),
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
        }

        public static UserSignInputModel GetUserSignInputModelDefault()
        {
            return new(){ Email = "a@a.ru", Password = "12345678" };
        }
        public static UserSignInputModel GetUserSignInputModelByEmailAndPassword(string email, string password)
        {
            return new(){ Email = email, Password = password };
        }
    }
}