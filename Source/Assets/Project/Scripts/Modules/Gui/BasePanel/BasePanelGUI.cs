using System;
using UnityEngine;

namespace Cofradinn.Modules.Gui
{

    public class BasePanelGUI : MonoBehaviour
    {
        [Header("Base Components")]
        [SerializeField] private BasePanelData _basePanelData;

        public bool _isEnabled { get { return _basePanelData._isEnabled; } set { _basePanelData._isEnabled = value; } }
        private Animator _myAnimator { get { return _basePanelData._myAnimator; } set { _basePanelData._myAnimator = value; } }
        private GameObject _view { get { return _basePanelData._view; } set { _basePanelData._view = value; } }
        private AnimationClip _myShowAnimationClip { get { return _basePanelData._myShowAnimationClip; } set { _basePanelData._myShowAnimationClip = value; } }
        private Action<string> _action;

        public void _ShowPanelInvoke(float onTime)
        {
            Invoke("_ShowPanel", onTime);
        }
        public void _ShowPanel()
        {
            _ShowPanelWithAnimation(true);
        }
        public void _HidePanelInvoke(float onTime)
        {
            Invoke("_HidePanel", onTime);
        }
        public void _HidePanel()
        {
            _ShowPanelWithAnimation(false);
        }
        public void _SetActiveView(bool active)
        {
            if (_view != null) _view.SetActive(active);
        }
        public void _ShowPanel(bool active, Action<string> action)
        {
            _action = action;
            if (active)
                _ShowPanel();
            else
                _HidePanel();
        }

        private void _ShowPanelWithAnimation(bool active)
        {
            _SetActiveView(true);

            if (!IsInvoking())
            {
                if (active)
                {
                    if (_myShowAnimationClip != null)
                    {
                        float timeNextAnimationClip = _myShowAnimationClip.length;
                        _myAnimator.SetBool("ToShow", true);
                        Invoke("_ShowFinished", timeNextAnimationClip);
                    }
                    else
                    {
                        _ShowFinished();
                    }
                }
                else
                {
                    if (_myShowAnimationClip != null)
                    {
                        float timeNextAnimationClip = _myShowAnimationClip.length;
                        _myAnimator.SetBool("ToShow", false);
                        Invoke("_HideFinished", timeNextAnimationClip);
                    }
                    else
                    {
                        _HideFinished();
                    }
                }
                _isEnabled = active;
            }
        }
        private void _ShowFinished()
        {
            if (_action != null)
                _action("ShowFinished");
        }
        private void _HideFinished()
        {
            if (_action != null)
                _action("HideFinished");

            _SetActiveView(false);
        }
    }
}
