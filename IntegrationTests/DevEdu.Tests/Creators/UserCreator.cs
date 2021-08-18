using DevEdu.Core.Enums;
using DevEdu.Core.Models;

namespace DevEdu.Tests.Creators
{
    public class UserCreator : BaseCreator
    {
        public UserUpdateInfoOutPutModel UpdateUserById(string token)
        {
            var model = new UserUpdateInputModel();
            return new UserUpdateInfoOutPutModel();
        }

        public void AddRoleToUser(string token, int userId, Role role)
        {
        }
    }
}