using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class NotificationFacade
    {
        private NotificationCreator _creator;
        public NotificationFacade() { _creator = new NotificationCreator(); }
    }
}