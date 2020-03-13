using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public abstract class BaseConsole : MonoBehaviour
    {
        protected Rect rectMinimizedWindow = new Rect(30, 30, 50, 50);
        protected Rect rectMaximizedWindow = new Rect(0, 0, Screen.width, Screen.height);
        protected Rect currentRectWindow;
        public Vector2 scrollPosition;

        protected List<Log> logs = new List<Log>();
        protected bool _isMinimized = false;
        protected string _internalTextoMinBtn = "Minimize";

        protected readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>()
        {
            { LogType.Log,          Color.white },
            { LogType.Warning,      Color.yellow },
            { LogType.Error,        Color.red },
            { LogType.Exception,    Color.red },
            { LogType.Assert,       Color.white },
         };

        protected struct Log
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

        public abstract void __Log(string message, LogType type, Object obj, string stackTrace = "");
        // Update is called once per frame
        protected void __ConsoleWindow()
        {
            if (GUILayout.Button(_internalTextoMinBtn)) _isMinimized = !_isMinimized;
            _internalTextoMinBtn = (_isMinimized) ? "Minimize" : "Maximize";
            currentRectWindow = (_isMinimized) ? rectMaximizedWindow : rectMinimizedWindow;
        }
    }
}