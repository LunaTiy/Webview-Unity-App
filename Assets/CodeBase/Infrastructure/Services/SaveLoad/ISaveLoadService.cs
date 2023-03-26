using CodeBase.Infrastructure.Data;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void Save();
        SavedData Load();
    }
}