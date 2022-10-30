using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using System;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action OnLoaded = null)
            => _coroutineRunner.StartCoroutine(LoadScene(sceneName, OnLoaded));

        private IEnumerator LoadScene(string sceneName, Action OnLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                OnLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);

            while (waitNextScene.isDone == false)
                yield return null;

            OnLoaded?.Invoke();
        }
    }
}
