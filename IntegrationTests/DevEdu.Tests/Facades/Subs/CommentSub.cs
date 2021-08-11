using DevEdu.Core.Models;
using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class CommentSub
    {
        private CommentCreator _creator;
        public CommentSub()
        {
            _creator = new CommentCreator();
        }
    }
}