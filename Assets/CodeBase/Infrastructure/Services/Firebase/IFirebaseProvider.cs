namespace CodeBase.Infrastructure.Services.Firebase
{
    public interface IFirebaseProvider : IService
    {
        void InitializeFirebase();
        bool TryGetUrl(out string url);
    }
}