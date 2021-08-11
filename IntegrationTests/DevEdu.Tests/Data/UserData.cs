using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class UserData : BaseData
    {
        public static IEnumerable AdminCreatedUserByAllRoles()
        {
            yield return new object[] { Role.Admin, Role.Admin };
            yield return new object[] { Role.Admin, Role.Manager };
            yield return new object[] { Role.Admin, Role.Methodist };
            yield return new object[] { Role.Admin, Role.Student };
            yield return new object[] { Role.Admin, Role.Teacher };
            yield return new object[] { Role.Admin, Role.Tutor };
            yield return new object[] { Role.Admin, new List<Role>() { Role.Teacher, Role.Methodist } };
            yield return new object[] { Role.Admin, new List<Role>() { Role.Teacher, Role.Tutor } };
        }

        public static IEnumerable SignInByAllRoles()
        {
            yield return new object[] { Role.Admin };
            yield return new object[] { Role.Manager };
            yield return new object[] { Role.Methodist };
            yield return new object[] { Role.Student };
            yield return new object[] { Role.Teacher };
            yield return new object[] { Role.Tutor };
            yield return new object[] { new List<Role>() { Role.Teacher, Role.Methodist } };
            yield return new object[] { new List<Role>() { Role.Teacher, Role.Tutor } };
        }

        public static IEnumerable ManagerCreatedUserByRoleStudent()
        {
            yield return new object[] { Role.Manager, Role.Student };
        }

        public static UserInsertInputModel GetValidUserInsertInputModelForRegistration<T>(T data)
        {
            var rnd = _random.Next(1, 1000);
            return new()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Patronymic = "Patronymic",
                Email = $"a{rnd}@{DateTime.Now.Millisecond}a.ru",
                Username = "Username",
                Password = "12345678",
                ContractNumber = DateTime.Now.ToString(_dateFormat),
                City = (int)City.SaintPetersburg,
                BirthDate = $"08.08.2008",
                GitHubAccount = "Git.com",
                Photo = "http://zloo.com",
                PhoneNumber = "9999999",
                Roles = GetRoles(data)
            };
        }

        public static UserSignInputModel GetUserSignInputModelByEmailAndPassword(string email, string password)
        {
            return new() { Email = email, Password = password };
        }

        private static List<Role> GetRoles<T>(T data)
        {
            var roles = new List<Role>();
            if (data is Role role)
            {
                roles.Add(role);
            }
            else
            {
                roles.AddRange(data as List<Role>);
            }
            return roles;
        }
    }
}