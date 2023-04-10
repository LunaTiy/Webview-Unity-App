using System;
using System.Collections.Generic;

namespace CodeBase.Data.Diary
{
    [Serializable]
    public class Training
    {
        public string date;
        public string name;
        public List<Exercise> exercises = new();
    }
}