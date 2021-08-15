using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class HomeworkControllerSub
    {
        private readonly HomeworkControllerCreator _creator;
        public HomeworkControllerSub() { _creator = new HomeworkControllerCreator(); }
    }
}