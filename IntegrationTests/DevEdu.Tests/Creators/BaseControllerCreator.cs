using DevEdu.Core.Requests;
using RestSharp;

namespace DevEdu.Tests.Creators
{
    public abstract class BaseControllerCreator
    {
        protected const string BaseEndPoint = "https://localhost:44386/";
        protected string _endPoint;
        protected RequestHelper _requestHelper = new RequestHelper();
        protected RestClient _client = new RestClient(BaseEndPoint);
    }
}