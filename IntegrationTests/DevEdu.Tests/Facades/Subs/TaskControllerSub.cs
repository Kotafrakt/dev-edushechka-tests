using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class TaskControllerSub
    {
        private TaskControllerCreator _creator;
        public TaskControllerSub() { _creator = new TaskControllerCreator(); }
    }
}