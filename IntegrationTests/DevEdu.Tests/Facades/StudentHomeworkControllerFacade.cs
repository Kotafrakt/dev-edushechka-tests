using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class StudentHomeworkControllerFacade
    {
        private StudentHomeworkCreator _creator;
        public StudentHomeworkControllerFacade() { _creator = new StudentHomeworkCreator(); }
    }
}
