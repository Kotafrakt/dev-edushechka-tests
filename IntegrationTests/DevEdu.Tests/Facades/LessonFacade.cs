using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class LessonFacade
    {
        private LessonCreator _creator;
        public LessonFacade() { _creator = new LessonCreator(); }
    }
}