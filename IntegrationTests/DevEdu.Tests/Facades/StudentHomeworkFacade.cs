using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class StudentHomeworkFacade
    {
        private StudentHomeworkCreator _creator;
        public StudentHomeworkFacade() { _creator = new StudentHomeworkCreator(); }
    }
}
