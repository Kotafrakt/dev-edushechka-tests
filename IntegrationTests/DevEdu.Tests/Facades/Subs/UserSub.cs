using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Tests.Fillings;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    internal class UserSub
    {
        private UserFilling _filling;

        public UserSub() { _filling = new UserFilling(); }

        internal UserSignInputModel RegisterUser(List<Role> roles)
        {
            return _filling.RegisterCorrectUser(roles);
        }
    }
}