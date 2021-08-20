using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class TaskFacade
    {
        private TaskCreator _creator;
        public TaskFacade() { _creator = new TaskCreator(); }
    }
}