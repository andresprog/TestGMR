using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Cofradinn.Modules.Utilities
{
    /// <summary>
    /// Files Sended by Fabian
    /// </summary>
    public class DebugConsole : BaseConsole
    {
        #region Singleton

        protected static DebugConsole instance;
        /// <summary>
        /// If true, the singleton won't be destroyed when the scene changes
        /// </summary>
        [Tooltip("If true, the singleton won't be destroyed when the scene changes")]
        [SerializeField]
        protected bool isPersistent = false;

        /// <summary>
        /// Returns the instance of this singleton.
        /// </summary>
        public static DebugConsole Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (DebugConsole)FindObjectOfType(typeof(DebugConsole));

                    if (instance == null)
                    {
#if THIS_DEBUG
                        Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but there is none. Generated automatically.");
#endif
                        GameObject obj = new GameObject("Singleton_" + typeof(DebugConsole));
                        instance = obj.AddComponent(typeof(DebugConsole)) as DebugConsole;

                    }
                }
                return instance;
            }
        }

        public static bool Exists
        {
            get { return !Object.ReferenceEquals(instance, null); }
        }
        public bool IsCurrentSingleton()
        {
            if (instance == null)
                return false;

            return instance.gameObject.GetInstanceID() == this.gameObject.GetInstanceID();
        }
        protected virtual void Awake()
        {
            //if (transform.parent != null) transform.parent = null;       

            if (instance != null && !IsCurrentSingleton())
            {
#if THIS_DEBUG
                Debug.LogWarning("Warning: More than one instance of singleton " + typeof(T) + " existing.");
#endif
                Destroy(this.gameObject);
            }

            else if (instance == null)
            {
                instance = gameObject.GetComponent<DebugConsole>(); // AddComponent(typeof(T)) as T;

                if (isPersistent)
                    DontDestroyOnLoad(gameObject);
            }
            OnAwake();
        }

        public virtual void OnDestroy()
        {
            if (IsCurrentSingleton())
            {
                instance = null;
            }
        }
        protected virtual void OnAwake()
        {
        }
        #endregion

        protected bool _showLine = false;
        protected bool _showName = false;

        public override void __Log(string message, LogType type, Object obj, string stackTrace = "")
        {
            string sms = "";

            if (_showName) sms = "Name: " + obj.name + "\n";

            sms += message;

            if (_showLine)
            {
                sms += "\n ____________________________________________________";
            }

            logs.Add(new Log(message, stackTrace, type));
        }
        private void __ConsoleWindow(int windowID)
        {
            base.__ConsoleWindow();

            if (_isMinimized)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

                for (int i = 0; i < logs.Count; i++)
                {
                    GUI.contentColor = logTypeColors[logs[i].type];
                    GUILayout.Label(logs[i].message);
                }

                GUILayout.EndScrollView();

                GUI.contentColor = Color.white;

                if (GUILayout.Button("Clear Console"))
                {
                    logs.Clear();
                }

                // Toggles
                // https://docs.unity3d.com/ScriptReference/GUILayout.Toggle.html
                _showLine = GUILayout.Toggle(_showLine, "Show Line");
                _showName = GUILayout.Toggle(_showName, "Show Object Name");
            }

            GUI.DragWindow(new Rect(0, 0, Mathf.Infinity, Mathf.Infinity));
            currentRectWindow.x = Mathf.Clamp(currentRectWindow.x, 0, Screen.width - currentRectWindow.width);
            currentRectWindow.y = Mathf.Clamp(currentRectWindow.y, 0, Screen.height - currentRectWindow.height);
        }
        private void OnGUI()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
#endif
                currentRectWindow = GUILayout.Window(123456, currentRectWindow, __ConsoleWindow, "Debug Console");
        }

    }
}