using DevEdu.Core.Requests;
using RestSharp;

namespace DevEdu.Tests.Creators
{
    public abstract class BaseCreator
    {
        protected const string BaseEndPoint = "https://localhost:44386/";
        protected string _endPoint;
        protected RestClient _client = new RestClient(BaseEndPoint);
        protected RequestHelper _requestHelper = new RequestHelper();
    }
}