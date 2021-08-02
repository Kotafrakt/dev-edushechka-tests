﻿using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Core.Requests
{
    public interface IDeleteRequest
    {
        Task<IRestResponse> DeleteAsync(string endPoint, Dictionary<string, string> headers);
    }
}