using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cofradinn.Modules.Utilities
{
    public class DebugClass : BaseConsole
    {
        #region Singleton

        protected static DebugClass instance;
        /// <summary>
        /// If true, the singleton won't be destroyed when the scene changes
        /// </summary>
        [Tooltip("If true, the singleton won't be destroyed when the scene changes")]
        [SerializeField]
        protected bool isPersistent = false;

        /// <summary>
        /// Returns the instance of this singleton.
        /// </summary>
        public static DebugClass _Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (DebugClass)FindObjectOfType(typeof(DebugClass));

                    if (instance == null)
                    {
#if THIS_DEBUG
                        Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but there is none. Generated automatically.");
#endif
                        GameObject obj = new GameObject("Singleton_" + typeof(DebugClass));
                        instance = obj.AddComponent(typeof(DebugClass)) as DebugClass;

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
                instance = gameObject.GetComponent<DebugClass>(); // AddComponent(typeof(T)) as T;

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

        private bool _showProperties = true;
        private bool _showfields = true;
        private bool _showPrivates = true;
        private bool _showPublics = true;
        private bool _showProtectes = true;

        private const string PUBLIC_FIELD = "public field";
        private const string PRIVATE_FIELD = "private field";
        private const string PROTECTED_FIELD = "protected field";
        private const string PUBLIC_PROPERTY = "public property";
        private const string PRIVATE_PROPERTY = "private property";
        private const string PROTECTED_PROPERTY = "protected property";
        private const string BACKING_FIELD = "BackingField";

        private static BindingFlags _propertiesTags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
        private static BindingFlags _fieldsTags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

        /// <summary>
        /// Call this method on LateUpdate
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        public override void __Log(string message, LogType type, Object obj, string stackTrace = "")
        {
            string sms = "";

            if (_showPublics) sms = "Name: " + obj.name + "\n";

            sms += message;

            if (_showPrivates)
            {
                sms += "\n ____________________________________________________";
            }

            logs.Add(new Log(sms, "", type));
        }
        public void __Log2(object obj)
        {
            string visibility = "";

            foreach (var propertyInfo in obj.GetType().GetProperties(_propertiesTags))
            {
                //CustomDebug._DebugPropertyInfo(propertyInfo);

                visibility = "";
                //https://docs.microsoft.com/en-us/dotnet/api/system.type.isnotpublic?view=netframework-4.8
                string message = propertyInfo.Name + ": " + propertyInfo.GetValue(obj).ToString();
                if (propertyInfo.GetMethod.IsPublic) visibility = PUBLIC_PROPERTY;
                if (propertyInfo.GetMethod.IsPrivate) visibility = PRIVATE_PROPERTY;
                if (propertyInfo.GetMethod.IsFamily) visibility = PROTECTED_PROPERTY;
                logs.Add(new Log(message, visibility, LogType.Log));
            }

            foreach (var fieldInfo in obj.GetType().GetFields(_fieldsTags))
            {
                //CustomDebug._DebugFieldInfo(fieldInfo);

                visibility = "";
                //https://docs.microsoft.com/en-us/dotnet/api/system.type.isnotpublic?view=netframework-4.8
                if (!fieldInfo.Name.EndsWith(BACKING_FIELD))
                {
                    string message = fieldInfo.Name + ": " + fieldInfo.GetValue(obj).ToString();
                    if (fieldInfo.IsPublic) visibility = PUBLIC_FIELD;
                    if (fieldInfo.IsPrivate) visibility = PRIVATE_FIELD;
                    if (fieldInfo.IsFamily) visibility = PROTECTED_FIELD;
                    logs.Add(new Log(message, visibility, LogType.Log));
                }
            }
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
                    if (_showfields)
                    {
                        if (_showPublics && logs[i].stackTrace == PUBLIC_FIELD) GUILayout.Label(logs[i].message);
                        if (_showPrivates && logs[i].stackTrace == PRIVATE_FIELD) GUILayout.Label(logs[i].message);
                        if (_showProtectes && logs[i].stackTrace == PROTECTED_FIELD) GUILayout.Label(logs[i].message);
                    }
                    if (_showProperties)
                    {
                        if (_showPublics && logs[i].stackTrace == PUBLIC_PROPERTY) GUILayout.Label(logs[i].message);
                        if (_showPrivates && logs[i].stackTrace == PRIVATE_PROPERTY) GUILayout.Label(logs[i].message);
                        if (_showProtectes && logs[i].stackTrace == PROTECTED_PROPERTY) GUILayout.Label(logs[i].message);
                    }
                }

                GUILayout.EndScrollView();
                GUI.contentColor = Color.white;

                // Buttons
                // https://docs.unity3d.com/ScriptReference/GUILayout.Button.html
                // if (GUILayout.Button("Line")) _line = !_line;
                // if (GUILayout.Button("GameObject")) _objectName = !_objectName;

                // Toggles
                // https://docs.unity3d.com/ScriptReference/GUILayout.Toggle.html
                _showPrivates = GUILayout.Toggle(_showPrivates, "Show Privates");
                _showPublics = GUILayout.Toggle(_showPublics, "Show Publics");
                _showProtectes = GUILayout.Toggle(_showProtectes, "Show Protectes");
                _showfields = GUILayout.Toggle(_showfields, "Show Fields");
                _showProperties = GUILayout.Toggle(_showProperties, "Show Properties");

                // Inputfields
                // https://docs.unity3d.com/ScriptReference/GUILayout.TextField.html
                //int n;
                //_spacing = GUILayout.TextField(_spacing, 99, "textfield");
                //bool isNumeric = int.TryParse(_spacing, out n);
                //if (!isNumeric || n < 17) _spacing = "17";
                //_internalSpacing = Converter.__ConvertStringToFloat(_spacing);
            }

            GUI.DragWindow(new Rect(0, 0, Mathf.Infinity, Mathf.Infinity));
            currentRectWindow.x = Mathf.Clamp(currentRectWindow.x, 0, Screen.width - currentRectWindow.width);
            currentRectWindow.y = Mathf.Clamp(currentRectWindow.y, 0, Screen.height - currentRectWindow.height);
        }
        private void Update()
        {
            logs.Clear();
        }
        private void OnGUI()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
#endif
                currentRectWindow = GUILayout.Window(123456, currentRectWindow, __ConsoleWindow, "Debug Classes Console");
        }
    }
}
