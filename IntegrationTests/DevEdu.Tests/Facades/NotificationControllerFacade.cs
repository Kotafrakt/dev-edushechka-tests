using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class NotificationControllerFacade
    {
        private NotificationCreator _creator;
        public NotificationControllerFacade() { _creator = new NotificationCreator(); }
    }
}