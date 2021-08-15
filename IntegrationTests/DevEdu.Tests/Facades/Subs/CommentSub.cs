using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class CommentSub
    {
        private CommentControllerCreator _creator;
        public CommentSub()
        {
            _creator = new CommentControllerCreator();
        }
    }
}