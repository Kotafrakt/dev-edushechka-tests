using DevEdu.Core.Models;

namespace DevEdu.Tests.Facades
{
    public class Facade
    {
        #region:Subs
        private readonly AuthenticationControllerSub _authenticationSub = new();
        private readonly CommentControllerSub _commentSub = new();
        private readonly CourseControllerSub _courseSub = new();
        private readonly GroupControllerSub _groupSub = new();
        private readonly HomeworkControllerSub _homeworkSub = new();
        private readonly LessonControllerSub _lessonSub = new();
        private readonly MaterialControllerSub _materialSub = new();
        private readonly NotificationControllerSub _notificationSub = new();
        private readonly PaymentControllerSub _paymentSub = new();
        private readonly StudentHomeworkControllerSub _studentHomeworkSub = new();
        private readonly TagControllerSub _tagTopicSub = new();
        private readonly TaskControllerSub _taskSub = new();
        private readonly TaskControllerSub _taskStudentSub = new();
        private readonly TopicControllerSub _topicSub = new();
        private readonly UserControllerSub _userSub = new();
        #endregion

        public string SignInByAdmin()
        {
            return _authenticationSub.GetTokenByEmailAndPassword(email: "Admin", password: "12345678");
        }

        public UserInfo SignInByAdminAndRegistrationNewUserByRole<T>(T roles)
        {
            var token = _authenticationSub.GetTokenByEmailAndPassword(email:"Admin", password:"12345678");
            var userInfo = _authenticationSub.RegisterUser(roles, token);
            userInfo.Token = token;
            return userInfo;
        }

        public UserInfo SignInByAdminAndRegistrationNewUserByRoleAndSignInByNewUser<T>(T roles)
        {
            var token = _authenticationSub.GetTokenByEmailAndPassword(email: "Admin", password: "12345678");
            var userInfo = _authenticationSub.RegisterUser(roles, token);
            userInfo.Token = _authenticationSub.GetTokenByEmailAndPassword(userInfo.Email, userInfo.Password);
            return userInfo;
        }

        public void LogOut(UserInfo userInfo)
        {
            userInfo.Token = null;
        }
    }
}