using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public class RotateAnimation : MonoBehaviour
    {
        [Tooltip("On/Off Animation")]
        [SerializeField] private bool _active;
        // Public Properties
        [Header("Types")]
        [Tooltip("Tipo de objeto que se va a resaltar")]
        [SerializeField] public ObjectType _objectType;
        [Tooltip("Tipo de efecto grafico que se quiere dar al objeto")]
        [SerializeField] public EfectType _efectType;
        [Tooltip("Tipo de animación")]
        [SerializeField] public AnimationType _animationType;
        [SerializeField] public Axis _axis;

        [Range(0, 1000)]
        [SerializeField] private float _speed;
        private float _aditionalSpeed;

        // Private References
        private Transform _target;
        private bool _up;
        [SerializeField] private Vector3 _targetAngle;
        private Vector3 _currentAngle;
        private Vector3 _startAngle;
        private StatusAnim _statusAnim;
        private bool _isStarted;



        /// <summary>
        /// Metodo que permite activar la animacion de resaltar elemento
        /// </summary>
        /// <param name="active"> "true" activa la animacion y "false" la desactiva</param>
        public void __RunAnimation(bool active)
        {
            //Debug.Log("Aqui animation" + active);
            this._active = active;
        }
        /// <summary>
        /// Asigna la velocidad de parpadeo, Range [0,100]
        /// </summary>
        public void __SetSpeed(float speed)
        {
            _speed = Mathf.Clamp(speed, 0, 100);
        }
        /// <summary>
        /// Asigna un nuevo angulo al objeto
        /// </summary>
        public void __SetNewTargetAngle(Vector3 angle)
        {
            _targetAngle = angle;
        }
        /// <summary>
        /// Asigna el angle por defecto del objeto
        /// </summary>
        public void __SetStartAngle()
        {
            _currentAngle = _startAngle;
            __ApplyAngleToTheObject(_currentAngle);
        }
        /// <summary>
        /// Asigna el angle de forma brusca, testear porque no se ha probado bien
        /// </summary>
        /// <param name="color"></param>
        public void __SetCurrentAngle(Vector3 angle)
        {
            _currentAngle = angle;
            __ApplyAngleToTheObject(_currentAngle);
            _statusAnim = StatusAnim.pause;
        }
        /// <summary>
        /// Retorna el angle por defecto del objeto
        /// </summary>
        /// <returns></returns>
        public Vector3 __GetStartAngle()
        {
            return _startAngle;
        }
        /// <summary>
        /// Retorna el tipo de objeto
        /// </summary>
        public ObjectType __GetObjectType()
        {
            return _objectType;
        }

        public void __Init()
        {
            __Init(this._targetAngle);
        }
        public void __Init(Vector3 targetAngle)
        {
            if (!_isStarted)
            {
                _isStarted = true;
                _aditionalSpeed = 1;
                _target = transform;
                _startAngle = __GetCurrentAngle();
                this._targetAngle = targetAngle;
                __ResetAnimationParameters();
            }
        }
        private void Awake()
        {
            __Init();
            //Debug.Log("Se esta asignando la velocidad de highlight en el start");
        }
        private void Update()
        {
            __IteratingTheAngle();
        }

        /// <summary>
        /// Reset the animation
        /// </summary>
        private void __ResetAnimationParameters()
        {
            _up = false;
            _statusAnim = StatusAnim.pause;
            _currentAngle = _startAngle;
            _active = false;
            __ApplyAngleToTheObject(_startAngle);
        }
        /// <summary>
        /// Iterating angle
        /// </summary>
        private void __IteratingTheAngle()
        {
            if (_active)
            {
                _statusAnim = StatusAnim.play;
                if (_animationType == AnimationType.Blink) __RunBlinkAnimation();
                if (_animationType == AnimationType.Static) __RunStaticAnimation();
            }
            else
            {
                if (_statusAnim == StatusAnim.play) __StoppingStaticAnimation();
            }
        }
        private void __RunBlinkAnimation()
        {
            if (_up)
            {
                if (_currentAngle != _targetAngle)
                {
                    _currentAngle = Vector3.MoveTowards(_currentAngle, _targetAngle, _speed * Time.unscaledDeltaTime * _aditionalSpeed);
                    __ApplyAngleToTheObject(_currentAngle);
                }
                else
                {
                    _up = false;
                }
            }
            else
            {
                if (_currentAngle != _startAngle)
                {
                    _currentAngle = Vector3.MoveTowards(_currentAngle, _startAngle, _speed * Time.unscaledDeltaTime * _aditionalSpeed);
                    __ApplyAngleToTheObject(_currentAngle);
                }
                else
                {
                    _up = true;
                }
            }
        }
        private void __StoppingStaticAnimation()
        {
            if (_currentAngle != _startAngle)
            {
               // _currentAngle = Vector3.MoveTowards(_currentAngle, _startAngle, _speed * Time.unscaledDeltaTime * _aditionalSpeed);
               // __ApplyAngleToTheObject(_currentAngle);

                // Turn towards our target rotation.
                Quaternion quaternionAngle = Quaternion.RotateTowards(Quaternion.Euler(_currentAngle), Quaternion.Euler(_startAngle), Time.deltaTime * _speed);
                _currentAngle = quaternionAngle.eulerAngles;
                __ApplyAngleToTheObject(_currentAngle);
            }
            else
            {
                __ResetAnimationParameters();
            }
        }
        private void __RunStaticAnimation()
        {
            // Old
            //_currentAngle = Vector3.MoveTowards(_currentAngle, _targetAngle, _speed * Time.deltaTime * _aditionalSpeed);
            //__ApplyAngleToTheObject(_currentAngle);

            // Turn towards our target rotation.
            Quaternion quaternionAngle = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(_targetAngle), Time.deltaTime * _speed);
            _currentAngle = quaternionAngle.eulerAngles;
            __ApplyAngleToTheObject(_currentAngle);

        }

        private void __ApplyAngleToTheObject(Vector3 angle)
        {
            switch (_objectType)
            {
                case ObjectType.None:
                default:
                    break;

                case ObjectType.GameObject:
                    if (_target != null) __SetSelectedAngle(angle);
                    else Debug.LogError("Controlled Error: Do not exist a Text component in this object: ", this);
                    break;

                    //case ObjectType.TMP_Text:
                    //    if (_tMP_Text != null) _tMP_Text.color = color;
                    //    else Debug.LogError("Controlled Error: Do not exist a TMP_Text component in this object: ", this);
                    //    break;
            }
        }
        /// <summary>
        /// Return the current object angle
        /// </summary>
        public Vector3 __GetCurrentAngle()
        {
            switch (_objectType)
            {
                case ObjectType.None:
                default:
                    Debug.LogError("Controlled Error: Alpha 0", this);
                    return Vector3.zero;

                case ObjectType.GameObject:
                    if (_target != null) return __GetSelectedAngle();
                    else Debug.LogError("Controlled Error: Do not exist a Text component in this object: ", this);
                    return Vector3.zero;
            }
        }
        /// <summary>
        /// Return true if the animation is active
        /// </summary>
        public bool __IsActive()
        {
            return _active;
        }

        private Vector3 __GetSelectedAngle()
        {
            return _target.transform.localEulerAngles;
        }
        private void __SetSelectedAngle(Vector3 angle)
        {
            _target.transform.localEulerAngles = angle;
        }
    }

    public enum ObjectType
    {
        None,
        GameObject,
    }

    public enum EfectType
    {
        None,
        Shake,
    }

    public enum AnimationType
    {
        None,
        Static,
        Blink,
    }
    public enum Axis
    {
        None,
        x,
        y,
        z,
    }
    public enum StatusAnim
    {
        play,
        pause,
    }
}

