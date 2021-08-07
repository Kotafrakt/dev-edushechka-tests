using System;

namespace DevEdu.Tests.Data
{
    public abstract class BaseData
    {
        protected const string _dateFormat = "MM/dd/yyyy hh:mm:ss.fff";
        protected static Random _random = new Random();
    }
}