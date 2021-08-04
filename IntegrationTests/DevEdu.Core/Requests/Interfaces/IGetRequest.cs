using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public interface IGetRequest
    {
        IRestResponse Get(IRestClient client, string endPoint, Dictionary<string, string> headers);
    }
}