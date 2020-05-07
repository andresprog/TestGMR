using UnityEngine;

namespace Cofradinn.Data.Gui.Loading.Utilities
{
    public class ExampleLoadingPanel : MonoBehaviour
    {
        public bool _active;

        private void Update()
        {
            LoadingImageSystem._Instance.__ShowSpinner(_active);
        }
    }
}