using System;
using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class CourseInfoShortOutputModel : CourseInfoBaseOutputModel
    {
        public string Description { get; set; }
        public List<GroupOutputMiniModel> Groups { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CourseInfoShortOutputModel model &&
                   Id == model.Id &&
                   Name == model.Name &&
                   Description == model.Description &&
                   EqualityComparer<List<GroupOutputMiniModel>>.Default.Equals(Groups, model.Groups);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, Groups);
        }
    }
}