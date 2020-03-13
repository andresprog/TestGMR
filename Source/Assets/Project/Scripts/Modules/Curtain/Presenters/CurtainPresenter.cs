using Cofradinn.Modules.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Modules.Curtain
{
    public class CurtainPresenter : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameObject _presenter;
        [SerializeField] private Image _imgCurtain;
        [SerializeField] private HighlightAnimation _highlightAnimation;

        public bool _IsActivated { get => _highlightAnimation._IsActivated; }
        public bool _IsDeactivated { get => _highlightAnimation._IsDeactivated; }

        public void __SetActivePresenter(bool active)
        {
            _presenter.SetActive(active);
        }
        public void __ShowCurtain(bool show)
        {
            _highlightAnimation.__SetActiveAnimationHighlight(show);
        }
        public void __SetSpeed(float speed)
        {
            _highlightAnimation.__SetSpeed(speed);
        }
        public void __BlockRaycast(bool block)
        {
            _imgCurtain.raycastTarget = block;
        }
    }
}
