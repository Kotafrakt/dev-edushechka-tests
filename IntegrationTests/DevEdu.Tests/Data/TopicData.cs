using DevEdu.Core.Models;
using System;

namespace DevEdu.Tests.Data
{
    public class TopicData : BaseData
    {
        public static TopicInputModel GetValidTopicInputModel()
        {
            return new()
            {
                Name = $"Topic {DateTime.Now.ToString(_dateFormat)}",
                Duration = DateTime.Now.Millisecond
            };
        }
    }
}