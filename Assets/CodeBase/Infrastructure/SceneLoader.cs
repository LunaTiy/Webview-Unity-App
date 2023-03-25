using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string nextScene, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(nextScene, onLoaded));

        private static IEnumerator LoadScene(string nextScene, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(nextScene);

            while (!loadSceneAsync.isDone)
                yield break;
            
            onLoaded?.Invoke();
        }
    }
}