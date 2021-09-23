using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    public class StudentHomeworkFacade
    {
        private StudentHomeworkCreator _creator;
        public StudentHomeworkFacade() { _creator = new StudentHomeworkCreator(); }
        internal List<StudentHomeworkWithHomeworkOutputModel> CreateListOfStudentAnswersHomework(int homeworkId, int groupId, int count = 3)
        {
            var answers = new List<StudentHomeworkWithHomeworkOutputModel>();
            var authenticationFacade = new AuthenticationFacade();
            var adminToken = authenticationFacade.SignInByAdmin();
            var groupFacade = new GroupFacade();
            for (int i = 0; i < count; i++)
            {
                var user = authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { Role.Student });
                groupFacade.AddUserToGroup(adminToken, groupId, user.Id, Role.Student);
                var studentToken = authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
                answers.Add(_creator.AddStudentHomework(studentToken, homeworkId));
            }
            return answers;
        }
    }
}
