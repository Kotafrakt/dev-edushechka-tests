using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class StudentHomeworkControllerSub
    {
        private StudentHomeworkControllerCreator _creator;
        public StudentHomeworkControllerSub() { _creator = new StudentHomeworkControllerCreator(); }
    }
}
