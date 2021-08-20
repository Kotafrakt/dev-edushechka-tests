using DevEdu.Core.Requests;
using RestSharp;

namespace DevEdu.Tests.Creators
{
    public abstract class BaseCreator
    {
        protected const string BaseEndEndpoint = "https://localhost:44386/";
        protected string _endPoint;
        protected RequestHelper _requestHelper = new RequestHelper();
        protected RestClient _client = new RestClient(BaseEndEndpoint);
    }
}