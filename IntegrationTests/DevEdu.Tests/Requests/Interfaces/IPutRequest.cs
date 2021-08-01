using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Tests.Requests
{
    public interface IPutRequest
    {
        Task<IRestResponse> PutAsync(string endPoint, Dictionary<string, string> headers, string jsonData);
    }
}