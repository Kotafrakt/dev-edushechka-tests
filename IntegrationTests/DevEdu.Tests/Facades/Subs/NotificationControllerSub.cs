using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class NotificationControllerSub
    {
        private NotificationControllerCreator _creator;
        public NotificationControllerSub() { _creator = new NotificationControllerCreator(); }
    }
}