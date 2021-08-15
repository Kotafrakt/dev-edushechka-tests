using DevEdu.Core.Models;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class NotificationControllerCreator : BaseControllerCreator
    {
        public NotificationInfoOutputModel AddNotification(string token)
        {
            var model = new NotificationAddInputModel();
            return new NotificationInfoOutputModel();
        }

        public NotificationInfoOutputModel UpdateNotification(string token, int notificationId)
        {
            var model = new NotificationUpdateInputModel();
            return new NotificationInfoOutputModel();
        }
    }
}