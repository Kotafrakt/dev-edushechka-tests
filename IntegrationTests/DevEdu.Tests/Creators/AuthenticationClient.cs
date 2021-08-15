﻿using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using Newtonsoft.Json;
using DevEdu.Tests.Constants;

namespace DevEdu.Tests.Creators
{
    public class AuthenticationClient : BaseCreator
    {
        public string SignInByEmailAndPasswordReturnToken(string email, string password)
        {
            _endPoint = AuthorizationPoints.SignInPoint;
            var postData = UserData.GetUserSignInputModelByEmailAndPassword(email, password);
            var request = _requestHelper.CreatePost(_endPoint, postData);
            return _client.Execute<string>(request).Data;
        }
    }
}