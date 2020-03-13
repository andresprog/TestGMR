using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Modules.Utilities
{
    public class HighlightAnimation : MonoBehaviour
    {
        public enum ObjectType
        {
            None,
            All,
            Slot,
            Box,
            Material,
            Image,
            Text,
            TMP_Text,
        }

        public enum EfectType
        {
            None,
            SolidColor,
            Alpha,
            Edge,
        }

        // Esto debería ser Highlight type
        public enum AnimationType
        {
            None,
            Static,
            Blink,
        }

        // AnimationType
        public enum SpeedType
        {
            None,
            Highlight,
            Curtain,
            //InSlowOutFast,
        }

        private enum StatusAnim
        {
            play,
            pause,
        }

        // Public Properties
        [Header("Types")]
        [Tooltip("Tipo de objeto que se va a resaltar")]
        [SerializeField] private ObjectType _objectType;
        [Tooltip("Tipo de efecto grafico que se quiere dar al objeto")]
        [SerializeField] private EfectType _efectType;
        [Tooltip("Tipo de animación")]
        [SerializeField] private AnimationType _animationType;
        [Tooltip("Tipo de velocidad")]
        [SerializeField] private SpeedType _speedType;

        [Header("Id Material")]
        [Tooltip("Es el id del material que queremos animar, es decir la posicion que ocupa el material en el mesh de este elemento")]
        [SerializeField] private int _idMeshMaterial;

        [Range(0, 100)]
        [SerializeField] private float _speed;
        [SerializeField] private bool _activeOnAwake;

        [Tooltip("Only Animation type Edge and SolidColor")]
        [SerializeField] private Color _targetColor;
        [Tooltip("On/Off Animation")]
        [SerializeField] private bool _active;

        // Private References
        private Renderer _renderer;
        private Image _image;
        private Text _text;
        private TMP_Text _tMP_Text;

        // Private fields
        private Color _startColor;
        private Color _currentColor;
        private bool _up;
        private StatusAnim _statusAnim;
        public bool _IsActivated { get; private set; }
        public bool _IsDeactivated { get; private set; }

        /// <summary>
        /// Metodo que permite activar la animacion de resaltar elemento
        /// </summary>
        /// <param name="active"> "true" activa la animacion y "false" la desactiva</param>
        public void __SetActiveAnimationHighlight(bool active)
        {
            //Debug.Log("Aqui animation" + active);

            //if (_speedType == SpeedType.Curtain && active)
            //    return;
            if (active)
            {
                _ResetAnimationParameters();
            }
            else
            {
                if (_speedType == SpeedType.Curtain)

                    _BalanceAnimationParameters();
                else
                    _ResetAnimationParameters();
            }

            _active = active;
        }
        /// <summary>
        /// No se que coño hace este metodo, porque es un parche de mierda, ni siquiera se que hace aquí!!!
        /// </summary>
        /// <param name="active"></param>
        public void _SetActiveFilmSlot(bool active)
        {
            // "Activa" el film de forma simple (sin animación)
            GetComponent<MeshRenderer>().gameObject.SetActive(active);
        }
        /// <summary>
        /// Asigna la velocidad de parpadeo, Range [0,100]
        /// </summary>
        public void __SetSpeed(float speed)
        {
            _speed = Mathf.Clamp(speed, 0, 100);
        }
        /// <summary>
        /// Asigna un nuevo color de highlight
        /// </summary>
        public void _SetNewTargetColor(Color color)
        {
            _targetColor = color;
        }
        /// <summary>
        /// Asigna el color por defecto del objeto
        /// </summary>
        public void _SetStartColor()
        {
            _currentColor = _startColor;
            _ApplyColorToTheObject(_currentColor);
        }
        /// <summary>
        /// Asigna el color de forma brusca, testear porque no se ha probado bien
        /// </summary>
        /// <param name="color"></param>
        public void _SetCurrentColor(Color color)
        {
            _currentColor = color;
            _ApplyColorToTheObject(color);
            _statusAnim = StatusAnim.pause;
        }
        /// <summary>
        /// Retorna el color por defecto del objeto
        /// </summary>
        /// <returns></returns>
        public Color _GetStartColor()
        {
            return _startColor;
        }
        /// <summary>
        /// Retorna el tipo de objeto
        /// </summary>
        public ObjectType _GetObjectType()
        {
            return _objectType;
        }


        public void _Init()
        {
            _Init(this._targetColor);
        }
        public void _Init(Color targetColor)
        {
            switch (_objectType)
            {
                case ObjectType.None:
                case ObjectType.All:
                default:
                    break;

                case ObjectType.Slot:
                case ObjectType.Box:
                case ObjectType.Material:
                    if (transform.GetComponent<Renderer>() != null) _renderer = transform.GetComponent<Renderer>();
                    else Debug.LogError("Controlled Error: Do not exist a Renderer component in this object: ", this);
                    break;

                case ObjectType.Image:
                    if (transform.GetComponent<Image>() != null) _image = transform.GetComponent<Image>();
                    else Debug.LogError("Controlled Error: Do not exist a Image component in this object: ", this);
                    break;

                case ObjectType.Text:
                    if (transform.GetComponent<Text>() != null) _text = transform.GetComponent<Text>();
                    else Debug.LogError("Controlled Error: Do not exist a Text component in this object: ", this);
                    break;

                case ObjectType.TMP_Text:
                    if (transform.GetComponent<TMP_Text>() != null) _tMP_Text = transform.GetComponent<TMP_Text>();
                    else Debug.LogError("Controlled Error: Do not exist a TMP_Text component in this object: ", this);
                    break;
            }

            _startColor = _GetCurrentColor();

            this._targetColor = targetColor;
            _ResetAnimationParameters();
        }


        private void Awake()
        {
            _Init();

            if (_activeOnAwake)
                __SetActiveAnimationHighlight(true);
        }
        private void Update()
        {
            _IteratingTheColor();
        }

        /// <summary>
        /// Resetea la animación, hay que probar porque creo que no funciona bien...!
        /// </summary>
        private void _ResetAnimationParameters()
        {
            _IsActivated = false;
            _IsDeactivated = true;
            _up = false;
            _statusAnim = StatusAnim.pause;
            _currentColor = _startColor;
            _active = false;
            _ApplyColorToTheObject(_startColor);
        }
        private void _BalanceAnimationParameters()
        {
            _up = false;
            _statusAnim = StatusAnim.pause;
            _active = false;
        }
        /// <summary>
        /// Iterando el color
        /// </summary>
        private void _IteratingTheColor()
        {
            if (_speedType == SpeedType.Curtain)
            {
                if (_active)
                {
                    _statusAnim = StatusAnim.play;

                    if (_animationType == AnimationType.Blink)
                    {
                        if (_up)
                        {
                            if (_currentColor != _targetColor)
                            {
                                //_currentColor = Color.LerpUnclamped(_renderer.materials[idMeshMaterial].color, targetColor, _speed);
                                _currentColor = Color.LerpUnclamped(_currentColor, _targetColor, _speed * Time.unscaledDeltaTime);
                                _ApplyColorToTheObject(_currentColor);
                            }
                            else
                            {
                                _up = false;
                            }
                        }
                        else
                        {
                            if (_currentColor != _startColor)
                            {
                                _currentColor = Color.Lerp(_currentColor, _startColor, _speed * Time.deltaTime);
                                _ApplyColorToTheObject(_currentColor);
                            }
                            else
                            {
                                _up = true;
                            }
                        }
                    }
                    if (_animationType == AnimationType.Static)
                    {
                        //_currentColor = Color.LerpUnclamped(_renderer.materials[idMeshMaterial].color, targetColor, _speed);
                        _IsDeactivated = false;
                        if (_currentColor != _targetColor) _IsActivated = true;
                        _currentColor = Color.LerpUnclamped(_currentColor, _targetColor, _speed * Time.deltaTime);
                        _ApplyColorToTheObject(_currentColor);
                    }
                }
                else
                {
                    if (_animationType == AnimationType.Static)
                    {
                        //_currentColor = Color.LerpUnclamped(_renderer.materials[idMeshMaterial].color, targetColor, _speed);
                        _currentColor = Color.LerpUnclamped(_currentColor, _startColor, _speed * Time.deltaTime);
                        _ApplyColorToTheObject(_currentColor);
                        _IsActivated = false;
                        if (_currentColor != _startColor) _IsDeactivated = true;
                        //if (_currentColor.a == _startColor.a)
                        //{
                        //    if (_statusAnim == StatusAnim.play)
                        //    {
                        //        _statusAnim = StatusAnim.pause;
                        //        _ResetAnimationParameters();
                        //    }
                        //}
                    }


                }
            }
            else
            {
                if (_active)
                {
                    _statusAnim = StatusAnim.play;

                    if (_animationType == AnimationType.Blink)
                    {
                        if (_up)
                        {
                            if (_currentColor != _targetColor)
                            {
                                //_currentColor = Color.LerpUnclamped(_renderer.materials[idMeshMaterial].color, targetColor, _speed);
                                _currentColor = Color.LerpUnclamped(_currentColor, _targetColor, _speed * Time.unscaledDeltaTime);
                                _ApplyColorToTheObject(_currentColor);
                            }
                            else
                            {
                                _up = false;
                            }
                        }
                        else
                        {
                            if (_currentColor != _startColor)
                            {
                                _currentColor = Color.Lerp(_currentColor, _startColor, _speed * Time.deltaTime);
                                _ApplyColorToTheObject(_currentColor);
                            }
                            else
                            {
                                _up = true;
                            }
                        }
                    }
                    if (_animationType == AnimationType.Static)
                    {
                        _IsDeactivated = false;
                        if (_currentColor != _targetColor) _IsActivated = true;
                        //_currentColor = Color.LerpUnclamped(_renderer.materials[idMeshMaterial].color, targetColor, _speed);
                        _currentColor = Color.LerpUnclamped(_currentColor, _targetColor, _speed * Time.deltaTime);
                        _ApplyColorToTheObject(_currentColor);

                    }
                }
                else
                {
                    // si el estado de la animacion es play 
                    if (_statusAnim == StatusAnim.play)
                    {
                        _IsActivated = false;
                        _IsDeactivated = true;
                        _statusAnim = StatusAnim.pause;
                        _ResetAnimationParameters();
                    }
                }
            }

        }
        /// <summary>
        /// Apply color to the object 
        /// </summary>
        private void _ApplyColorToTheObject(Color color)
        {
            switch (_objectType)
            {
                case ObjectType.None:
                case ObjectType.All:
                default:
                    break;

                case ObjectType.Slot:
                case ObjectType.Box:
                case ObjectType.Material:
                    if (_renderer != null) _renderer.materials[_idMeshMaterial].color = color;
                    else Debug.LogError("Controlled Error: Do not exist a Renderer component in this object: ", this);
                    break;

                case ObjectType.Image:
                    if (_image != null) _image.color = color;
                    else Debug.LogError("Controlled Error: Do not exist a Image component in this object: ", this);
                    break;

                case ObjectType.Text:
                    if (_text != null) _text.color = color;
                    else Debug.LogError("Controlled Error: Do not exist a Text component in this object: ", this);
                    break;

                case ObjectType.TMP_Text:
                    if (_tMP_Text != null) _tMP_Text.color = color;
                    else Debug.LogError("Controlled Error: Do not exist a TMP_Text component in this object: ", this);
                    break;
            }
        }
        /// <summary>
        /// Retorna el color actual del objeto
        /// </summary>
        public Color _GetCurrentColor()
        {
            switch (_objectType)
            {
                case ObjectType.None:
                case ObjectType.All:
                default:
                    Debug.LogError("Controlled Error: Alpha 0", this);
                    return Color.clear;

                case ObjectType.Slot:
                case ObjectType.Box:
                case ObjectType.Material:
                    if (transform.GetComponent<Renderer>() != null) return _renderer.materials[_idMeshMaterial].color;
                    else Debug.LogError("Controlled Error: Do not exist a Renderer component in this object: ", this);
                    return Color.clear;

                case ObjectType.Image:
                    if (transform.GetComponent<Image>() != null) return _image.color;
                    else Debug.LogError("Controlled Error: Do not exist a Image component in this object: ", this);
                    return Color.clear;

                case ObjectType.Text:
                    if (transform.GetComponent<Text>() != null) return _text.color;
                    else Debug.LogError("Controlled Error: Do not exist a Text component in this object: ", this);
                    return Color.clear;

                case ObjectType.TMP_Text:
                    if (transform.GetComponent<TMP_Text>() != null) return _tMP_Text.color;
                    else Debug.LogError("Controlled Error: Do not exist a TMP_Text component in this object: ", this);
                    return Color.clear;
            }
        }
        /// <summary>
        /// Indica si la animacion esta activa
        /// </summary>
        public bool _IsActive()
        {
            return _active;
        }
    }
}
