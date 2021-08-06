using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Core.Requests
{
    public interface IDeleteRequest
    {
        IRestRequest Delete(IRestClient client, string endPoint, Dictionary<string, string> headers);
    }
}