using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class CommentControllerSub
    {
        private CommentControllerCreator _creator;
        public CommentControllerSub()
        {
            _creator = new CommentControllerCreator();
        }
    }
}