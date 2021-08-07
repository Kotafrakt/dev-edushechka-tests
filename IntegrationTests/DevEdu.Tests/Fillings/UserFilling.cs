using DevEdu.Core.Enums;
using DevEdu.Tests.Data;
using System.Collections.Generic;

namespace DevEdu.Tests.Fillings
{
    public class UserFilling : BaseApi
    {
        public void RegisterCorrectUser(List<Role> roles)
        {
            var postData = UserData.GetUserInsertInputModelForRegistration_Correct(roles);

        }
    }
}