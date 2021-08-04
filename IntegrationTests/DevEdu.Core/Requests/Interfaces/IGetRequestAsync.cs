﻿using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Core.Requests
{
    public interface IGetRequestAsync
    {
        Task<IRestResponse> GetAsync(string endPoint, Dictionary<string, string> headers);
    }
}