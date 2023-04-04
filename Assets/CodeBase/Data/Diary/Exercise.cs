using System;
using System.Collections.Generic;

namespace CodeBase.Data.Diary
{
    [Serializable]
    public class Exercise
    {
        public string name;
        public List<Set> sets;
    }
}