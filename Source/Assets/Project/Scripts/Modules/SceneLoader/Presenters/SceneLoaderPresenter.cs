using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Modules.SceneLoader
{
    public class SceneLoaderPresenter : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameObject _presenter;
        [SerializeField] private Text _lblTips;
        [SerializeField] private Text _lblContinueMessage;
        [SerializeField] private Image _progressBar;

        public void __SetBarValue(float value)
        {
            _progressBar.fillAmount = value;
        }
        public void __SetActivePresenter(bool active)
        {
            _presenter.SetActive(active);
        }
        public void __SetActiveContinueMessage(bool active)
        {
            _lblContinueMessage.gameObject.SetActive(active);
        }
    }
}
