using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using System;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class UserData : BaseData
    {
        public static UserInsertInputModel GetUserInsertInputModelForRegistration(List<Role> roles)
        {
            var rnd1 = _random.Next(1, 9);
            var rnd2 = _random.Next(1, 1000);
            return new()
            {
                FirstName = "FirstName" + DateTime.Now.Millisecond.ToString(),
                LastName = "LastName" + DateTime.Now.Millisecond.ToString(),
                Patronymic = "Patronymic" + DateTime.Now.Millisecond.ToString(),
                Email = $"a{rnd1}@{rnd2}a.ru",
                Username = "Username" + DateTime.Now.Millisecond.ToString(),
                Password = "12345678",
                ContractNumber = DateTime.Now.ToString(_dateFormat),
                City = (int)City.SaintPetersburg,
                BirthDate = $"0{rnd1}.0{rnd1}.200{rnd1}",
                GitHubAccount = "Git.com",
                Photo = "http://zloo.com",
                PhoneNumber = "9999999",
                Roles = roles
            };
        }

        public static UserSignInputModel GetUserSignInputModelByEmailAndPassword(string email, string password)
        {
            return new() { Email = email, Password = password };
        }
    }
}