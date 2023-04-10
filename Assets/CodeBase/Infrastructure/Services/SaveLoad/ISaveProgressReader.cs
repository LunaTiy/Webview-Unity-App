using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISaveProgressReader
    {
        void Load(SavedData savedData);
    }
}