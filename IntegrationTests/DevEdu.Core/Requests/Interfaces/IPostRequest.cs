﻿using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public interface IPostRequest
    {
        IRestResponse Post(string endPoint, Dictionary<string, string> headers, string jsonData);
    }
}