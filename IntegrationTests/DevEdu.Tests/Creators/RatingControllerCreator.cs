using DevEdu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEdu.Tests.Creators
{
    public class RatingControllerCreator : BaseControllerCreator
    {
        public StudentRatingOutputModel AddStudentRating(string token)
        {
            var model = new StudentRatingInputModel();
            return new StudentRatingOutputModel();
        }

        public StudentRatingOutputModel UpdateStudentRating(string token, int userId, int value, int periodNumber)
        {
            return new StudentRatingOutputModel();
        }
    }
}