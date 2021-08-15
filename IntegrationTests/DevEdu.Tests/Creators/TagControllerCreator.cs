using DevEdu.Core.Models;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class TagControllerCreator : BaseControllerCreator
    {
        public TagOutputModel AddTag(string token)
        {
            var model = new TagInputModel();
            return new TagOutputModel();
        }

        public TagOutputModel UpdateTag(string token, int tagId)
        {
            var model = new TagInputModel();
            return new TagOutputModel();
        }
    }
}