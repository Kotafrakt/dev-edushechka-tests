using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using Newtonsoft.Json;
using RestSharp;
using System;
using static DevEdu.Tests.Constants.GroupEndpoints;

namespace DevEdu.Tests.Data
{
    public class GroupData : BaseData
    {
        protected const string BaseEndpoint = "https://localhost:44386/";
        public static GroupInputModel GetValidGroupInputModel(int courseId, GroupStatus status = GroupStatus.Learning)
        {
            return new()
            {
                Name = $"BestPeopleEver{_random.Next(1, 1000)}",
                CourseId = courseId,
                GroupStatusId = status,
                StartDate = DateTime.Now.AddDays(_random.Next(-60, -10)).ToString(_validDateFormat),
                Timetable = $"Timetable {_random.Next(0, 1000)}",
                PaymentPerMonth = _random.Next(2, 6) * 500
            };
        }
        public static GroupInputModel GetInValidGroupInputModel(int courseId, GroupStatus status = GroupStatus.Learning)
        {
            
            return new()
            {
                Name = $"BestPeopleEver{_random.Next(1, 1000)}",
                CourseId = courseId,
                GroupStatusId = status,
                StartDate = DateTime.Now.AddDays(_random.Next(-60, -10)).ToString(),
                Timetable = $"Timetable {_random.Next(0, 1000)}",
                PaymentPerMonth = _random.Next(2, 6) * 500
            };
        }

        public static GroupOutputModel CreateGroupInDbByAdminAndGetModel(GroupInputModel postData, string adminToken)
        {
            var _endPoint = AddGroupEndpoint;
            var _requestHelper = new RequestHelper();
            var _client = new RestClient(BaseEndpoint);
            var requestPost = _requestHelper.CreatePostRequest(_endPoint, postData, adminToken);
            var postResponce = _client.Execute(requestPost);
            var outputModelByResponce = JsonConvert.DeserializeObject<GroupOutputModel>(postResponce.Content);
            return outputModelByResponce;
        }
    }
}