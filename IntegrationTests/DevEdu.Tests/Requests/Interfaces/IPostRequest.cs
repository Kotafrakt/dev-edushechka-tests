using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Tests.Requests
{
    public interface IPostRequest
    {
        Task<IRestResponse> PostAsync(string endPoint, Dictionary<string, string> headers, string jsonData);
    }
}