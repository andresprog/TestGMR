using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public class TimerClock360 : MonoBehaviour
    {
        public enum TimeMode
        {
            DeltaTime, // es afectado por la pausa de time.timeScale = 0
            UnscaleTimeScale, // no es afectado
        }

        //#region Testing

        //public Command _command = Command.None;
        //public float _Testtime;

        //private void FixedUpdate()
        //{
        //    _Testtime = _time;
        //    _SetCommand(_command);
        //    _command = Command.None;
        //}

        //#endregion
        [Header("Timer Afected by TimeScale")]
        [Tooltip("Esta constante nos indica si el timer es afectado por las pausas del time.timeScale = 0")]
        public TimeMode _timeMode;
        [Header("Read Only")]
        [SerializeField] private State _state;
        [SerializeField] private float _time;
        [SerializeField] private bool _isEventActive;
        [Header("First Event")]
        [SerializeField] private TimerEvent _currentTimerEvent;
        [Header("All Events")]
        [SerializeField] private List<TimerEvent> _timerEvents;


        public enum State
        {
            None,

            Running,
            Stoped,
            Paused,

            All,
        }

        public enum Command
        {
            None,

            Play,
            Stop,
            Pause,

            All,
        }

        private void Awake()
        {
            Init();
        }

        public TimerClock360()
        {
            Init();
        }

        private void Init()
        {
            _time = 0;
            _timerEvents = new List<TimerEvent>();
        }
        public void _SetCommand(Command command)
        {
            switch (command)
            {
                case Command.Play: _Play(); break;
                case Command.Stop: _Stop(); break;
                case Command.Pause: _Pause(); break;
                default: break;
            }
        }
        /// <summary>
        /// Metodo que genera un evento cuando el timer sobrepasa el valor
        /// </summary>
        /// <param name="processDelegate"></param>
        /// <param name="time"></param>
        public void _SetEventTime(ProcessDelegate processDelegate, EventProcessDto eventProcessDto)
        {
            TimerEvent timerEvent = new TimerEvent(processDelegate, eventProcessDto);

            if (_timerEvents == null)
            {
                _timerEvents = new List<TimerEvent>();
            }

            _timerEvents.Add(timerEvent);

            if (_timerEvents.Count >= 2)
            {
                _timerEvents = _timerEvents.OrderBy(x => x._eventProcessDto._eventTime).ToList();
            }

            // ordenar lista por tiempos, de menor a mayor
            //for (int i = 0; i < _timerEvents.Count; i++)
            //{
            //    Debug.Log("n: " + i);
            //    Debug.Log("item._time: " + _timerEvents[i]._eventProcessDto._eventTime);
            //    Debug.Log("item._emptyMethod: " + _timerEvents[i]._processDelegate);
            //}
            _currentTimerEvent = _timerEvents[0];
            _isEventActive = true;
        }
        public float _GetTime()
        {
            return _time;
        }
        public State _GetState()
        {
            return _state;
        }
        public bool _IsRunnig()
        {
            return (_state == State.Running);
        }
        public void _DisableEventTime()
        {
            _isEventActive = false;
            _timerEvents = null;
            _currentTimerEvent = null;
        }

        private void Update()
        {
            //Debug.Log("Time: " + Time.time);
            //Debug.Log("Delta Time: " + Time.deltaTime);
            //Debug.Log("UnscaledTime: " + (int)Time.unscaledTime);
            //Debug.Log("UnscaledDeltaTime: " + Time.unscaledDeltaTime);
            //Debug.Log("FixedUnscaledTime: " + Time.fixedUnscaledTime);
            //Debug.Log("FixedUnscaledTime: " + Time.fixedTime);

            if (_state == State.Running)
            {
                CalculateTime();

                if (_isEventActive)
                {
                    if (_time > _currentTimerEvent._eventProcessDto._eventTime)
                    {
                        //_isEventActive = false;
                        _currentTimerEvent._processDelegate(_currentTimerEvent._eventProcessDto);
                        _currentTimerEvent = _GetNextTimerEvent();
                        //if (!_isEventActive)
                        //{
                        //    _DisableEventTime();
                        //}
                    }
                }
            }
            //Debug.Log("time: " + _time);
        }
        private void CalculateTime()
        {
            if (_timeMode == TimeMode.UnscaleTimeScale)
            {
                _time += Time.unscaledTime * 0.001f;
            }
            else
            {
                _time += Time.deltaTime;
            }
        }
        private void _Play()
        {
            if (_state != State.Running)
            {
                _state = State.Running;
            }
        }
        private void _Pause()
        {
            if (_state == State.Running)
            {
                _state = State.Paused;
            }
        }
        private void _Stop()
        {
            if (_state != State.Stoped)
            {
                _time = 0;
                _state = State.Stoped;
            }
        }

        private TimerEvent _GetNextTimerEvent()
        {
            if (_timerEvents.Count > 0)
            {
                _timerEvents.RemoveAt(0);
                if (_timerEvents.Count > 0)
                {
                    return _timerEvents[0];
                }
                else
                {
                    _DisableEventTime();
                    return null;
                }
            }
            else
            {
                _DisableEventTime();
                return null;
            }
        }

        [System.Serializable]
        private class TimerEvent
        {
            public ProcessDelegate _processDelegate;
            public EventProcessDto _eventProcessDto;

            public TimerEvent(ProcessDelegate processDelegate, EventProcessDto eventProcessDto)
            {
                _processDelegate = processDelegate;
                _eventProcessDto = eventProcessDto;
            }
        }

    }


    public delegate void ProcessDelegate(EventProcessDto eventProcessDto);

    [System.Serializable]
    public class EventProcessDto
    {
        [Header("Settings")]

        [HideInInspector] public string _key;
        public Type _type;
        public Action _action;
        public float _eventTime;
        public string _description;

        public EventProcessDto(Type type, Action action, float eventTime, string description, string _key)
        {
            _type = type;
            _action = action;
            _eventTime = eventTime;
            _description = description;
        }

        public EventProcessDto()
        {
            _type = Type.None;
            _action = Action.None;
            _eventTime = 0;
            _description = string.Empty;
        }

        public enum Type
        {
            None = 0,
            Standart = 1,

            Start = 2,
            End = 3,

            Information = 4,
            Question = 6,
        }

        public enum Action
        {
            None = 0,
            Standart = 1,

            Show = 2,
            Hide = 3,
        }
    }
}