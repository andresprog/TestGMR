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
    public class DebugPort : MonoBehaviour
    {
        public Rect _rectWindow = new Rect(100, 100, 500, 500);
        public Vector2 _scrollPosition;
        private List<Log> _logs;

        private static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>()
        {
            { LogType.Log,          Color.white },
            { LogType.Warning,      Color.yellow },
            { LogType.Error,        Color.red },
            { LogType.Exception,    Color.red },
            { LogType.Assert,       Color.white },
        };

        private struct Log
        {
            public Log(string message, string stackTrace, LogType type)
            {
                this.message = message;
                this.stackTrace = stackTrace;
                this.type = type;
            }

            public string message;
            public string stackTrace;
            public LogType type;
        }

        private void Start()
        {
            Application.logMessageReceived += __HandleLog;
            //Application.RegisterLogCallback(__HandleLog);
        }
        private void OnGUI()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
#endif
                _rectWindow = GUILayout.Window(123456, _rectWindow, __ConsoleWindow, "Console");
        }
        private void __ConsoleWindow(int windowID)
        {
            if (_logs == null) return;

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, true);

            for (int i = 0; i < _logs.Count; i++)
            {
                GUI.contentColor = logTypeColors[_logs[i].type];
                GUILayout.Label(_logs[i].message);
            }

            GUILayout.EndScrollView();

            GUI.contentColor = Color.white;

            if (GUILayout.Button("Clear"))
            {
                _logs.Clear();
            }


            GUI.DragWindow(new Rect(0, 0, Mathf.Infinity, Mathf.Infinity));
            _rectWindow.x = Mathf.Clamp(_rectWindow.x, 0, Screen.width - _rectWindow.width);
            _rectWindow.y = Mathf.Clamp(_rectWindow.y, 0, Screen.height - _rectWindow.height);
        }
        private void __HandleLog(string message, string stackTrace, LogType type)
        {
           // _logs.Add(new Log(message, stackTrace, type));
        }
    }
}