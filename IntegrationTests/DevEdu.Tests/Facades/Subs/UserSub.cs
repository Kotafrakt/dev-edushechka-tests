using DevEdu.Core.Enums;
using DevEdu.Tests.Fillings;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    internal class UserSub
    {
        private UserFilling _filling;

        public UserSub()
        {
            _filling = new UserFilling();
        }
        internal void RegisterUser(List<Role> roles)
        {
            _filling.RegisterCorrectUser(roles);
        }
    }
}