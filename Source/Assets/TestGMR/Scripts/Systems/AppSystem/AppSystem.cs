using UnityEngine;

namespace Cofradinn.Systems
{
    public class AppSystem : SingletonComponent<AppSystem>
    {
        protected override void OnAwake()
        {
        }

        /// <summary>
        /// Close App
        /// </summary>
        public void __Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         UnityEngine.Application.OpenURL(webplayerQuitURL);
#else
         UnityEngine.Application.Quit();
#endif
        }
    }
}