using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    internal class UserSub
    {
        private UserCreator _creator;

        public UserSub() { _creator = new UserCreator(); }

        internal UserSignInputModel RegisterUser(List<Role> roles)
        {
            return _creator.RegisterCorrectUser(roles);
        }
    }
}