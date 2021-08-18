using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class RatingControllerFacade
    {
        private RatingCreator _creator;
        public RatingControllerFacade(){ _creator = new RatingCreator(); }
    }
}
