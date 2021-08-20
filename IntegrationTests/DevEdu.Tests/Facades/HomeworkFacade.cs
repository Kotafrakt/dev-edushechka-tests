using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class HomeworkFacade
    {
        private readonly HomeworkCreator _creator;
        public HomeworkFacade() { _creator = new HomeworkCreator(); }
    }
}