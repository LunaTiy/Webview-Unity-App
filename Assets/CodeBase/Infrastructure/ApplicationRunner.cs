using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class ApplicationRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrapper _bootstrapperPrefab;
        
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<Bootstrapper>();

            if (bootstrapper == null)
                Instantiate(_bootstrapperPrefab);
        }
    }
}