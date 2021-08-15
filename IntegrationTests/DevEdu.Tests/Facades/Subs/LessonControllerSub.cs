using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class LessonControllerSub
    {
        private LessonControllerCreator _creator;
        public LessonControllerSub() { _creator = new LessonControllerCreator(); }
    }
}