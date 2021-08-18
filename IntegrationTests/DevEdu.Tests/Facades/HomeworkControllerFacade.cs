using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class HomeworkControllerFacade
    {
        private readonly HomeworkCreator _creator;
        public HomeworkControllerFacade() { _creator = new HomeworkCreator(); }
    }
}