#if UNITY_EDITOR
using Cofradinn.Utilities.IconsReplacer;
#endif
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    /// <summary>
    /// Remember to call base.Awake() in each new Singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [Header("Singleton properties")]
        /// <summary>
        /// If true, the singleton won't be destroyed when the scene changes
        /// </summary>
        [Tooltip("If true, the singleton won't be destroyed when the scene changes")]
        [SerializeField] protected bool isPersistent = false;

        /// <summary>
        /// The lock to prevent concurrency problems.
        /// </summary>
        private static object _lock = new object();
        /// <summary>
        /// Returns the instance of this singleton.
        /// </summary>
        public static T _Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (_instance == null)
                        {
#if THIS_DEBUG
                        Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but there is none. Generated automatically.");
#endif
                            GameObject obj = new GameObject("Singleton_" + typeof(T).Name);
                            _instance = obj.AddComponent(typeof(T)) as T;

                        }
                    }
                    return _instance;
                }
            }
        }
        protected static T _instance;
        public static bool _Exists
        {
            get { return !Object.ReferenceEquals(_instance, null); }
        }

        public bool __IsCurrentSingleton()
        {
            if (_instance == null)
                return false;

            return _instance.gameObject.GetInstanceID() == this.gameObject.GetInstanceID();
        }
        protected virtual void Awake()
        {
            //if (transform.parent != null) transform.parent = null;       

            if (_instance != null && !__IsCurrentSingleton())
            {
#if THIS_DEBUG
                Debug.LogWarning("Warning: More than one instance of singleton " + typeof(T) + " existing.");
#endif
                Destroy(this.gameObject);
            }

            else if (_instance == null)
            {
                _instance = gameObject.GetComponent<T>(); // AddComponent(typeof(T)) as T;

                if (isPersistent)
                    DontDestroyOnLoad(gameObject);
            }
            OnAwake();
        }
        public virtual void OnDestroy()
        {
            if (__IsCurrentSingleton())
            {
                _instance = null;
            }
        }
        protected virtual void OnAwake()
        {
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            IconsReplacer.__PutMyIcon(this, IconType.System);
        }
#endif
    }

    /// <summary>
    /// Remember to call base.Awake() in each new Singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMediator<T> : Singleton<T> where T : MonoBehaviour
    {
#if UNITY_EDITOR
        private new void OnDrawGizmos()
        {
            IconsReplacer.__PutMyIcon(this, IconType.Mediator);
        }
#endif
    }
}

/// <summary>
/// Base class to provide a singleton status to an object.
///
/// According to Wikipedia:
/// "The singleton pattern is a design pattern that restricts the instantiation of a class to one object.
/// This is useful when exactly one object is needed to coordinate actions across the system."
/// All singletons have also an option that can be marked in the Inspector to keep them alive whenever the current scene changes in Unity.
/// </summary>