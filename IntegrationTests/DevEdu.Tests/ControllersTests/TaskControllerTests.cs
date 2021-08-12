using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using DevEdu.Tests.Facades;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEdu.Tests.ControllersTests
{
    public class TaskControllerTests : BaseControllerTest
    {
        private UserSub _userSub = new UserSub();
        private GroupSub _groupSub = new GroupSub();
        private TagSub _tagSub = new TagSub();
        private AuthenticationSub _authenticationSub = new AuthenticationSub();

        [TestCase(Role.Teacher)]
        [TestCase(Role.Admin)]
        public void AddTaskByTeacher(Role role)
        {
            //Given
            var user = _userSub.RegisterUser(new List<Role> { role });
            var token = _authenticationSub.GetTokenByEmailAndPassword(user.Email, user.Password);
            var group = _groupSub.CreateValidGroup(token);
            var tags = _tagSub.CreateValidTagList(token);
            var task = TaskData.GetValidTaskByTeacherWithHomework(group.Id, tags.Select(tag => tag.Id).ToList());
            _endPoint = TaskPoints.AddTaskByTeacherPoint;
            var request = _requestHelper.Post(_endPoint, task);
            request = _requestHelper.Autorize(request, token);

            //When
            var actual = _client.Execute<TaskOutputModel>(request).Data;

            //Then
            //task.Should().BeEquivalentTo(actual, option => option.Excluding(o => o.)  )
        }

    }
}
