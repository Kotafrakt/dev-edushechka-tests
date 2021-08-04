﻿using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Core.Requests
{
    public interface IDeleteRequestAsync
    {
        Task<IRestResponse> DeleteAsync(IRestClient client, string endPoint, Dictionary<string, string> headers);
    }
}