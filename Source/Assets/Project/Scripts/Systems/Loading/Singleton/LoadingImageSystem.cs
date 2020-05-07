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

        /// <summary>
        /// Active the spinner
        /// </summary>
        /// <param name="active"></param>
        public void __ShowSpinner(bool active)
        {
            _view.SetActive(active);
        }
        protected override void OnAwake()
        {
            __ShowSpinner(false);
        }
    }
}
