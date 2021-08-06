using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public interface IPutRequest
    {
        IRestRequest Put(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData);
    }
}