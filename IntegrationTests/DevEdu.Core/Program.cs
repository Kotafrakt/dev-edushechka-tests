using RestSharp;
using System;
using System.Collections.Generic;

namespace DevEdu.Core
{
    class Program
    {
        private static RestClient _client = new("https://localhost:44386/api");
        static void Main(string[] args)
        {
            var request = new RestRequest("Default", Method.GET);
            IRestResponse<List<string>> response =
            _client.Execute<List<string>>(request);
            Console.ReadKey();
        }
    }
}