using DevEdu.Core.Requests;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace DevEdu.Tests.Fillings
{
    public abstract class BaseFilling
    {
        protected const string BaseEndPoint = "https://localhost:44386/";
        protected string _endPoint;
        protected Dictionary<string, string> _headers = new Dictionary<string, string>();
        protected RestClient _client = new RestClient(BaseEndPoint);
        protected RequestHelper _requestHelper = new RequestHelper();

        protected void AuthenticateClient(string token)
        {
            CleanHeader();
            _headers.Add("Authorization", $"Bearer {token}");
        }

        protected void CleanHeader()
        {
            _headers.Clear();
        }
    }
}