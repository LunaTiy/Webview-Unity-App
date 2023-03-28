using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Loading
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private float _fadeTimeStep = 0.03f;
        [SerializeField] private float _fadeInStep = 0.2f;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide()
            => StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            var waitForSeconds = new WaitForSeconds(_fadeTimeStep);

            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _fadeInStep;
                yield return waitForSeconds;
            }

            gameObject.SetActive(false);
        }
    }
}