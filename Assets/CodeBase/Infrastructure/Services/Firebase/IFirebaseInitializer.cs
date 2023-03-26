using System.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Firebase
{
    public interface IFirebaseInitializer : IService
    {
        Task Initialize();
        bool TryGetUrl(out string url);
    }
}