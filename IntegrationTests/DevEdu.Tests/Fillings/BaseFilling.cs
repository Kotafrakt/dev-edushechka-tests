using DevEdu.Core.Requests;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.Fillings
{
    public abstract class BaseFilling
    {
        protected const string BaseEndPoint = "https://localhost:44386/";
        protected string _endPoint;
        protected string _token;
        protected Dictionary<string, string> _headers = new Dictionary<string, string>();
        protected RestClient _client = new RestClient(BaseEndPoint);
        protected RequestHelper _requestHelper = new RequestHelper();

        protected void AuthenticateClient()
        {
            CleanHeader();
            _headers.Add("Authorization", $"Bearer {_token}");
        }

        protected void CleanHeader()
        {
            _headers.Clear();
        }
        protected void CleanToken()
        {
            _token = string.Empty;
        }
    }
}