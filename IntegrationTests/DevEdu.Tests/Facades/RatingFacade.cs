using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class RatingFacade
    {
        private RatingCreator _creator;
        public RatingFacade(){ _creator = new RatingCreator(); }
    }
}
