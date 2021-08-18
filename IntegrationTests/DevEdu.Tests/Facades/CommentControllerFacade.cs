using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class CommentControllerFacade
    {
        private CommentCreator _creator;
        public CommentControllerFacade()
        {
            _creator = new CommentCreator();
        }
    }
}