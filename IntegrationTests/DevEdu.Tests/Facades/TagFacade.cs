using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    public class TagFacade
    {
        private readonly TagCreator _creator;
        public TagFacade() { _creator = new TagCreator(); }

        internal TagOutputModel AddTag(string token)
        {
            return _creator.AddTag(token);
        }

        public List<TagOutputModel> AddValidTagList(string token, int count = 3)
        {
            var tags = new List<TagOutputModel>();
            for (int i = 0; i < 3; i++)
            {
                tags.Add(_creator.AddTag(token));
            }
            return tags;
        }
    }
}