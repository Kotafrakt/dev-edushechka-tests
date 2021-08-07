using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using System;
using System.Collections.Generic;

namespace DevEdu.Tests.Data
{
    public class UserData : BaseData
    {
        public static UserInsertInputModel GetUserInsertInputModelForRegistration_Correct(List<Role> roles)
        {
            var rnd1 = _random.Next(1, 10);
            var rnd2 = _random.Next(1, 1000);
            return new()
            {
                FirstName = "FirstName" + DateTime.Now.Millisecond.ToString(),
                LastName = "LastName" + DateTime.Now.Millisecond.ToString(),
                Patronymic = "Patronymic" + DateTime.Now.Millisecond.ToString(),
                Email = "a@a.ru",
                Username = "Username" + DateTime.Now.Millisecond.ToString(),
                Password = "12345678",
                ContractNumber = DateTime.Now.ToString(_dateFormat),
                City = _random.Next(1, 4),
                BirthDate = $"{rnd1}.{rnd1}.20{rnd1}",
                GitHubAccount = "Git.com",
                Photo = "http://zloo.com",
                PhoneNumber = "9999999",
                Roles = roles
            };
        }
    }
}