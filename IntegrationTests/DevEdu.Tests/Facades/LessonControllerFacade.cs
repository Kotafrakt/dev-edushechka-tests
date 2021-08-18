using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class LessonControllerFacade
    {
        private LessonCreator _creator;
        public LessonControllerFacade() { _creator = new LessonCreator(); }
    }
}