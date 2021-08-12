using DevEdu.Core.Models;
using System.Collections.Generic;
﻿using DevEdu.Tests.Creators;

namespace DevEdu.Tests.Facades
{
    internal class TaskSub
    {
        private readonly TaskCreator _creator;
        public TaskSub() { _creator = new TaskCreator(); }

        internal TaskInfoOutputModel CreateCorrectTaskByTeacherWithoutHomework(string token, List<int> tagIds = default)
        {
            return _creator.CreateCorrectTaskByTeacherWithoutHomework(token, tagIds);
        }
    }
}