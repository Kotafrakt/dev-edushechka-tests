using DevEdu.Core.Requests;
using Newtonsoft.Json;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.Fillings
{
    public class AuthenticationClient : BaseFilling
    {
        public string SignInByEmailAndPassword_ReturnToken(string email, string password)
        {
            _endPoint = SignInPoint;
            var postData = AuthenticationControllerData.GetUserSignInputModelByEmailAndPassword(email, password);
            var jsonData = JsonConvert.SerializeObject(postData);
            _headers.Add("content-type", "application/json");
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            return _client.Execute<string>(request).Data;
        }
    }
}