using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Curtain
{
    public interface ICurtainHandler
    {
        State _state { get; }
        bool _IsActivated { get; }
        bool _IsDeactivated { get; }

        void __ShowCurtain(bool show);
        void __ShowCurtain(bool show, float speed);
    }

    public class CurtainHandler : MonoBehaviour, ICurtainHandler
    {
        [Header("Components")]
        [SerializeField] private CurtainPresenter _curtainPresenter;

        public bool _IsActivated { get => _curtainPresenter._IsActivated; }
        public bool _IsDeactivated { get => _curtainPresenter._IsDeactivated; }
        public State _state { get; private set; }

        public void __ShowCurtain(bool show, float speed)
        {
            _curtainPresenter.__SetSpeed(speed);
            __ShowCurtain(show);
        }
        public void __ShowCurtain(bool show)
        {
            _state = (show) ? State.Showing : State.Hiding;
            _curtainPresenter.__BlockRaycast(show);
            _curtainPresenter.__ShowCurtain(show);
        }
    }
}
