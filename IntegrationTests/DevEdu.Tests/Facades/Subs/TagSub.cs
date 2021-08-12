using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    internal class TagSub
    {
        private TagCreator _creator;

        public TagSub() { _creator = new TagCreator(); }

        public TagOutputModel CreateValidTag(string token)
        {
            return _creator.CreateValidTag(token);
        }

        public List<TagOutputModel> CreateValidTagList(string token, int count = 3)
        {
            var tags = new List<TagOutputModel>();
            for (int i = 0; i < 3; i++)
            {
                tags.Add(_creator.CreateValidTag(token));
            }
            return tags;
        }
    }
}