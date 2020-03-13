using Cofradinn.AppEnums;
using Cofradinn.Systems;
using UnityEngine;

namespace Cofradinn.Loading.Example
{
    public class CallLoadingScene : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private SceneName _goToScene;
        [Header("Test Commands")]
        [SerializeField] private bool _ChangeScene;

        public void __ChangeScene()
        {
        }

        private void Update()
        {
            if (_ChangeScene)
            {
                _ChangeScene = false;
                __ChangeScene();
            }
        }
    }
}
