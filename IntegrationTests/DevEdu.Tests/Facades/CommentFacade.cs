using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    public class CommentFacade
    {
        private CommentCreator _creator;
        public CommentFacade()
        {
            _creator = new CommentCreator();
        }
    }
}