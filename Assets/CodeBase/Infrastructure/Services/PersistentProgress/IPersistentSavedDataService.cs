using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentSavedDataService : IService
    {
        SavedData SavedData { get; set; }
    }
}