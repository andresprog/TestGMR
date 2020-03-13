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
    public class DebugVariables : BaseConsole
    {
        #region Singleton

        protected static DebugVariables instance;
        /// <summary>
        /// If true, the singleton won't be destroyed when the scene changes
        /// </summary>
        [Tooltip("If true, the singleton won't be destroyed when the scene changes")]
        [SerializeField]
        protected bool isPersistent = false;

        /// <summary>
        /// Returns the instance of this singleton.
        /// </summary>
        public static DebugVariables Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (DebugVariables)FindObjectOfType(typeof(DebugVariables));

                    if (instance == null)
                    {
#if THIS_DEBUG
                        Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but there is none. Generated automatically.");
#endif
                        GameObject obj = new GameObject("Singleton_" + typeof(DebugVariables));
                        instance = obj.AddComponent(typeof(DebugVariables)) as DebugVariables;

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
                instance = gameObject.GetComponent<DebugVariables>(); // AddComponent(typeof(T)) as T;

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

        private void Update()
        {
            logs.Clear();
        }

        //private static string _spacing = "10";
        //private static float _internalSpacing = 10f;
        protected bool _showLine = false;
        protected bool _showName = false;

        /// <summary>
        /// Call this method on LateUpdate
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        public override void __Log(string message, LogType type, Object obj, string stackTrace = "")
        {
            string sms = "";

            if (_showName) sms = "Name: " + obj.name + "\n";

            sms += message;

            if (_showLine)
            {
                sms += "\n ____________________________________________________";
            }

            logs.Add(new Log(sms, "", type));
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

                    // GUILayoutOption (Style)
                    // https://answers.unity.com/questions/702499/what-is-a-guilayoutoption.html
                    //GUILayoutOption[] gUILayoutOption = new GUILayoutOption[]
                    //{
                    //    GUILayout.Height(_internalSpacing),

                    //};
                    //GUILayout.Label(logs[i].message, gUILayoutOption);

                    GUILayout.Label(logs[i].message);
                }

                GUILayout.EndScrollView();
                GUI.contentColor = Color.white;

                // Buttons
                // https://docs.unity3d.com/ScriptReference/GUILayout.Button.html
                // if (GUILayout.Button("Line")) _line = !_line;
                // if (GUILayout.Button("GameObject")) _objectName = !_objectName;

                // Toggles
                // https://docs.unity3d.com/ScriptReference/GUILayout.Toggle.html
                _showLine = GUILayout.Toggle(_showLine, "Show Line");
                _showName = GUILayout.Toggle(_showName, "Show Object Name");

                // Inputfields
                // https://docs.unity3d.com/ScriptReference/GUILayout.TextField.html
                //int n;
                //_spacing = GUILayout.TextField(_spacing, 99, "textfield");
                //bool isNumeric = int.TryParse(_spacing, out n);
                //if (!isNumeric || n < 17) _spacing = "17";
                //_internalSpacing = Converter.__ConvertStringToFloat(_spacing);
            }

            GUI.DragWindow(new Rect(0, 0, Mathf.Infinity, Mathf.Infinity));
            currentRectWindow.x = Mathf.Clamp(currentRectWindow.x, 0, Screen.width - 1 /*currentRectWindow.width*/);
            currentRectWindow.y = Mathf.Clamp(currentRectWindow.y, 0, Screen.height - 1 /*currentRectWindow.height*/);
        }

        private void OnGUI()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
#endif
                currentRectWindow = GUILayout.Window(123456, currentRectWindow, __ConsoleWindow, "Debug Variables Console");
        }
    }
}
