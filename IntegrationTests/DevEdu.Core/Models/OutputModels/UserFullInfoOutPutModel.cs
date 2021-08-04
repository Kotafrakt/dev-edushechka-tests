using DevEdu.Core.Enums;

namespace DevEdu.Core.Models
{
    public class UserFullInfoOutPutModel : UserInfoOutPutModel
    {
        public string Username { get; set; }
        public string RegistrationDate { get; set; }
        public string ContractNumber { get; set; }
        public string BirthDate { get; set; }
        public string GitHubAccount { get; set; }
        public string PhoneNumber { get; set; }
        public string ExileDate { get; set; }
        public City City { get; set; }
    }
}