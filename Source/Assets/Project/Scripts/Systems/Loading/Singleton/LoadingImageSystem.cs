using Cofradinn.Modules.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Data.Gui.Loading.Utilities
{
    public class LoadingImageSystem : SingletonComponent<LoadingImageSystem>
    {
        [Header("Components")]
        [SerializeField] private GameObject _view;

        public void _ShowLoadingImage(bool active)
        {
            _view.SetActive(active);
        }
        protected override void OnAwake()
        {
            _ShowLoadingImage(false);
        }
    }
}
