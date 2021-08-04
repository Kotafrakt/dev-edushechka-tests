﻿using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Core.Requests
{
    public interface IPutRequestAsync
    {
        Task<IRestResponse> PutAsync(string endPoint, Dictionary<string, string> headers, string jsonData);
    }
}