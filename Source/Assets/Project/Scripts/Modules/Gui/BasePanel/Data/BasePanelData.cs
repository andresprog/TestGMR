using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Gui
{
    [Serializable]
    public class BasePanelData
    {
        public Animator _myAnimator;
        public AnimationClip _myShowAnimationClip;
        public GameObject _view;
        public bool _isEnabled;
    }
}
