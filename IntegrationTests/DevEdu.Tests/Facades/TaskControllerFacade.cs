using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class TaskControllerFacade
    {
        private TaskCreator _creator;
        public TaskControllerFacade() { _creator = new TaskCreator(); }
    }
}