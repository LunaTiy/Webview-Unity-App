using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public class PersistentSavedDataService : IPersistentSavedDataService
    {
        public SavedData SavedData { get; set; }
    }
}