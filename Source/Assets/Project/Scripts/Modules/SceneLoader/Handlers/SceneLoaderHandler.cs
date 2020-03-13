using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cofradinn.Systems;

namespace Cofradinn.Modules.SceneLoader
{
    public interface ISceneLoaderHandler
    {
        bool _UserQuitLoadingPanel { get; }
        bool _SceneIsLoaded { get; }

        void __ShowSceneLoadingPanel(bool active);
        void __LoadScene(SceneName nextScene);
        void __LoadMainMenu();
    }

    public class SceneLoaderHandler : MonoBehaviour, ISceneLoaderHandler
    {
        [Header("Components")]
        [SerializeField] private SceneLoaderPresenter _sceneLoaderPresenter;
        [Header("Parameters")]
        [SerializeField] private string[] _advices;

        public bool _UserQuitLoadingPanel { get; private set; }
        public bool _SceneIsLoaded { get; private set; }

        private int _random = 0;
        private SceneName _previousSceneName;
        private SceneName _nextSceneName;

        public void __LoadScene(SceneName nextScene)
        {
            _sceneLoaderPresenter.__SetActiveContinueMessage(false);
            _previousSceneName = _nextSceneName;
            _nextSceneName = nextScene;
            StartCoroutine(___LoadScene());
        }
        public void __LoadMainMenu()
        {
            __LoadScene(SceneName.MainMenu);
        }
        private IEnumerator ___LoadScene()
        {
            _SceneIsLoaded = false;
            _UserQuitLoadingPanel = false;

            yield return new WaitForSecondsRealtime(1f);

            AsyncOperation async = SceneManager.LoadSceneAsync(_nextSceneName.ToString());
            async.allowSceneActivation = false;

            while (!async.isDone)
            {
                float barValue = Mathf.Clamp01(async.progress / .9f);
                _sceneLoaderPresenter.__SetBarValue(barValue);

                if (async.progress >= 0.9f)
                {
                    _SceneIsLoaded = true;
                    _sceneLoaderPresenter.__SetActiveContinueMessage(true);
                    if (Input.anyKeyDown)
                    {
                        _UserQuitLoadingPanel = true;
                        async.allowSceneActivation = true;
                    }
                }
                yield return null;
            }
        }

        public void __ShowSceneLoadingPanel(bool active)
        {
            _sceneLoaderPresenter.__SetActivePresenter(active);
        }
    }
}