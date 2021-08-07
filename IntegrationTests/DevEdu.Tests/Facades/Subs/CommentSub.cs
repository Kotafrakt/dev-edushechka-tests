using DevEdu.Core.Models;
using DevEdu.Tests.Fillings;

namespace DevEdu.Tests.Facades
{
    internal class CommentSub
    {
        private CommentFilling filling;
        public CommentSub()
        {
            filling = new CommentFilling();
        }
        internal CommentAddInputModel CommentAddInputModel_Correct()
        {
            return new()
            {
                Text = "Пример"
            };
        }

        internal CommentAddInputModel CommentAddInputModel_TextNull()
        {
            return new()
            {
                Text = null
            };
        }

        internal CommentAddInputModel CommentAddInputModel_Null()
        {
            return null;
        }
    }
}