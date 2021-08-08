using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using Newtonsoft.Json;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.Fillings
{
    public class AuthenticationClient : BaseFilling
    {
        public string SignInByEmailAndPassword_ReturnToken(string email, string password)
        {
            _headers.Clear();
            _endPoint = SignInPoint;
            var postData = UserData.GetUserSignInputModelByEmailAndPassword(email, password);
            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            return _client.Execute<string>(request).Data;
        }
    }
}