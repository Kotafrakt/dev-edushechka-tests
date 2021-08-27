using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DevEdu.Core.Models
{
    public class TaskInfoOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }
        public bool IsRequired { get; set; }
        public List<TagOutputModel> Tags { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is TaskInfoOutputModel model && Tags.Count == model.Tags.Count)
            {
                var tagsEquals = false;
                if (Tags == default)
                    tagsEquals = Tags == model.Tags;
                else
                {
                    tagsEquals = Tags.Intersect(model.Tags).ToList().Count == Tags.Count && Tags.Count == model.Tags.Count;
                }   
                return tagsEquals &&
                       Id == model.Id &&
                       Name == model.Name &&
                       Description == model.Description &&
                       Links == model.Links &&
                       IsRequired == model.IsRequired &&
                       IsDeleted == model.IsDeleted;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, Links, IsRequired, Tags, IsDeleted);
        }
    }
}