using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Core.Requests
{
    public interface IPostRequestAsync
    {
        Task<IRestResponse> PostAsync(IRestClient client, string endPoint, Dictionary<string, string> headers, string jsonData);
    }
}