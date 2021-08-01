using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Tests.Requests
{
    public interface IGetRequest
    {
        Task<IRestResponse> GetAsync(string endPoint, Dictionary<string, string> headers);
    }
}