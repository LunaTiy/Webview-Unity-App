using System;
using System.Collections.Generic;

namespace CodeBase.Data.Diary
{
    [Serializable]
    public class Training
    {
        public string date;
        public float humanWeight;
        public List<Exercise> exercises;
    }
}