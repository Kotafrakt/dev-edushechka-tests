using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using DevEdu.Tests.Facades;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DevEdu.Tests.ControllersTests
{
    public class TaskControllerTests : BaseControllerTest
    {
        private GroupFacade _groupFacade;
        private TagFacade _tagFacade;
        private AuthenticationFacade _authenticationFacade;

        public TaskControllerTests() : base()
        {
            _groupFacade = new GroupFacade();
            _tagFacade = new TagFacade();
            _authenticationFacade = new AuthenticationFacade();
        }

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void AddTaskByTeacher(Role role)
        {
            //Given
            var token = _authenticationFacade.SignInByAdmin();
            var group = _groupFacade.CreateValidGroup(token);
            var tags = _tagFacade.AddValidTagList(token);
            var user = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role } );
            token = _authenticationFacade.GetTokenByEmailAndPassword(user.Email, user.Password);
            var task = TaskData.GetValidTaskByTeacherWithHomework(group.Id, tags.Select(tag => tag.Id).ToList());
            _endPoint = TaskEndpoints.AddTaskByTeacherEndpoint;
            var request = _requestHelper.CreatePostRequest(_endPoint, task, token);

            //When
            var actualResponce = _client.Execute<TaskOutputModel>(request);

            //Then
            task.Should().BeEquivalentTo(actualResponce.Data);
            //, option => option.Excluding(o => o.))
        }

    }
}
