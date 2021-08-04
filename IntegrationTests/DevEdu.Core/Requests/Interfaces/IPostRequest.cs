using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public interface IPostRequest
    {
        IRestResponse Post(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData);
    }
}