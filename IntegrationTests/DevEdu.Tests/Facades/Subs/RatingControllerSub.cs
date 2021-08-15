using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class RatingControllerSub
    {
        private RatingControllerCreator _creator;
        public RatingControllerSub(){ _creator = new RatingControllerCreator(); }
    }
}
