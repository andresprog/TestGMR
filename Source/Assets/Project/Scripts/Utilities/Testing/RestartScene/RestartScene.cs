using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cofradinn.Modules.Utilities
{
    public class RestartScene : MonoBehaviour
    {
        public void OnclikRestartScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }
}
