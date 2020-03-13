using Cofradinn.Modules.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inheriting from this class makes any script a singleton.
/// To be able to use it, the singleton script must be used in the root object of a prefab
/// saved in Prefabs/Resources/Systems/ named exactly the same as the script.
/// For example the Foo singleton script must exist in the root object of a prefab
/// saved as Prefabs/Resources/Systems/Foo.
/// This script lazily instantiates the prefab (if not alredy present in the scene)
/// the first time Instance is called.
/// </summary>
public abstract class SingletonComponent<T> : MonoBehaviour where T : SingletonComponent<T>
{
    /// <summary>
    /// If true, the singleton won't be destroyed when the scene changes
    /// </summary>
    [Header("Singleton properties")]
    [Tooltip("If true, the singleton won't be destroyed when the scene changes")]
    [SerializeField] protected bool isPersistent = false;
    
    private static T instance;
    public static T _Instance
    {
        get
        {
            if (instance == null)
            {
                // First try to find our object in the scene.
                instance = FindObjectOfType<T>();

                // If it is not there...
                if (instance == null)
                {
                    // We load the prefab...
                    GameObject prefab = Prefab;
                    if (prefab == null)
                    {
                        Debug.LogError("The prefab of the singleton " + typeof(T) + " is not named correctly.\n" +
                        	"Check the Resources/Systems folders.");
                    }
                    else
                    {
                        // And instantiate it.
                        instance = Instantiate(Prefab).GetComponent<T>();

                        if (instance == null)
                        {
                            Debug.LogError("The prefab of the singleton " + typeof(T) + " must have a " + typeof(T) + " component in its root object.\n" +
                            	"Check the Resources/Systems folders.");
                        }
                    }
                }
            }

            return instance;
        }
    }

    private static GameObject Prefab
    {
        get => Resources.Load<GameObject>("Systems/" + typeof(T).Name.ToString());
    }

    public void Awake()
    {
        if (!Equals(_Instance))
        {
#if THIS_DEBUG
                Debug.LogWarning("Warning: More than one instance of singleton " + typeof(T) + " existing.");
#endif
            Destroy(this.gameObject);
        }
        else
        {
            if (isPersistent)
            {
                transform.SetParent(null);
                DontDestroyOnLoad(this.gameObject);
            }
            OnAwake();
        }
    }

    protected abstract void OnAwake();
}
