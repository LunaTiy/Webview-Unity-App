using CodeBase.Infrastructure.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentSavedDataService : IService
    {
        SavedData SavedData { get; set; }
    }
}