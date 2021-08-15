﻿using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.Creators
{
    public class TagMaterialCreator : BaseCreator
    {
        public TagOutputModel CreateTagCorrect(string token)
        {
            AuthenticateClient(token);

            _endPoint = AddTagPoint;
            var postData = TagData.GetTagInputModel_Correct();

            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute<TagOutputModel>(request);
            var result = response.Data;

            postData.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.IsDeleted));

            CleanHeader();
            return result;
        }
    }
}