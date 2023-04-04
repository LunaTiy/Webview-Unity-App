using System;
using CodeBase.Data.Diary;

namespace CodeBase.Data
{
    [Serializable]
    public class SavedData
    {
        public string url;
        public TrainingDiary trainingDiary;
    }
}