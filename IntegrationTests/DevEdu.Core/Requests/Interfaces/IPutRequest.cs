using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public interface IPutRequest
    {
        IRestResponse Put(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData);
    }
}