using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class UserData : BaseData
    {
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

        public static IEnumerable AdminCreatedUserByAllRoles()
        {
            yield return new object[] { UserRoleData.GetRoleAdmin(), UserRoleData.GetRoleAdmin() };
            yield return new object[] { UserRoleData.GetRoleAdmin(), UserRoleData.GetRoleManager() };
            yield return new object[] { UserRoleData.GetRoleAdmin(), UserRoleData.GetRoleMethodist() };
            yield return new object[] { UserRoleData.GetRoleAdmin(), UserRoleData.GetRoleTeacher() };
            yield return new object[] { UserRoleData.GetRoleAdmin(), UserRoleData.GetRoleStudent() };
            yield return new object[] { UserRoleData.GetRoleAdmin(), UserRoleData.GetRoleTutor() };
            yield return new object[] { UserRoleData.GetRoleAdmin(),
                new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleMethodist() } };
            yield return new object[] { UserRoleData.GetRoleAdmin(),
                new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleTutor() } };
        }

        public static IEnumerable SignInByAllRoles()
        {
            yield return new object[] { UserRoleData.GetRoleAdmin() };
            yield return new object[] { UserRoleData.GetRoleManager() };
            yield return new object[] { UserRoleData.GetRoleMethodist() };
            yield return new object[] { UserRoleData.GetRoleStudent() };
            yield return new object[] { UserRoleData.GetRoleTeacher() };
            yield return new object[] { UserRoleData.GetRoleTutor() };
            yield return new object[] {
                new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleMethodist() } };
            yield return new object[] {
                new List<Role>() { UserRoleData.GetRoleTeacher(), UserRoleData.GetRoleTutor() } };
        }

        public static IEnumerable ManagerCreatedUserByRoleStudent()
        {
            yield return new object[] { UserRoleData.GetRoleManager(), UserRoleData.GetRoleStudent() };
        }

        public static IEnumerable CreatedInvalidUserInsertModelByAdmin()
        {
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidBirthDate(UserRoleData.GetRoleAdmin()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidBirthDate(UserRoleData.GetRoleManager()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidBirthDate(UserRoleData.GetRoleMethodist()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidBirthDate(UserRoleData.GetRoleStudent()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidBirthDate(UserRoleData.GetRoleTeacher()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidBirthDate(UserRoleData.GetRoleTutor()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidLastName(UserRoleData.GetRoleAdmin()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidLastName(UserRoleData.GetRoleManager()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidLastName(UserRoleData.GetRoleMethodist()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidLastName(UserRoleData.GetRoleStudent()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidLastName(UserRoleData.GetRoleTeacher()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidLastName(UserRoleData.GetRoleTutor()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPassword(UserRoleData.GetRoleAdmin()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPassword(UserRoleData.GetRoleManager()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPassword(UserRoleData.GetRoleMethodist()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPassword(UserRoleData.GetRoleStudent()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPassword(UserRoleData.GetRoleTeacher()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPassword(UserRoleData.GetRoleTutor()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPhoto(UserRoleData.GetRoleAdmin()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPhoto(UserRoleData.GetRoleManager()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPhoto(UserRoleData.GetRoleMethodist()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPhoto(UserRoleData.GetRoleStudent()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPhoto(UserRoleData.GetRoleTeacher()) };
            yield return new object[] { UserRoleData.GetRoleAdmin(), GetUserInsertInputModelForRegistrationByInvalidPhoto(UserRoleData.GetRoleTutor()) };
        }

        public static IEnumerable CreatedInvalidUserInsertModelByManager()
        {
            yield return new object[] { UserRoleData.GetRoleManager(), GetUserInsertInputModelForRegistrationByInvalidBirthDate(UserRoleData.GetRoleStudent()) };
            yield return new object[] { UserRoleData.GetRoleManager(), GetUserInsertInputModelForRegistrationByInvalidLastName(UserRoleData.GetRoleStudent()) };
            yield return new object[] { UserRoleData.GetRoleManager(), GetUserInsertInputModelForRegistrationByInvalidPassword(UserRoleData.GetRoleStudent()) };
            yield return new object[] { UserRoleData.GetRoleManager(), GetUserInsertInputModelForRegistrationByInvalidPhoto(UserRoleData.GetRoleStudent()) };
        }

        private static UserInsertInputModel GetUserInsertInputModelForRegistrationByInvalidPassword<T>(T data)
        {
            var user = GetValidUserInsertInputModelForRegistration(data);
            user.Password = "123";
            return user;
        }

        private static UserInsertInputModel GetUserInsertInputModelForRegistrationByInvalidBirthDate<T>(T data)
        {
            var user = GetValidUserInsertInputModelForRegistration(data);
            user.BirthDate = "123";
            return user;
        }

        private static UserInsertInputModel GetUserInsertInputModelForRegistrationByInvalidLastName<T>(T data)
        {
            var user = GetValidUserInsertInputModelForRegistration(data);
            user.LastName = null;
            return user;
        }

        private static UserInsertInputModel GetUserInsertInputModelForRegistrationByInvalidPhoto<T>(T data)
        {
            var user = GetValidUserInsertInputModelForRegistration(data);
            user.Photo = "123";
            return user;
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