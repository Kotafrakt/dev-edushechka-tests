using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public interface IDeleteRequest
    {
        IRestResponse Delete(IRestClient client, string endPoint, Dictionary<string, string> headers);
    }
}