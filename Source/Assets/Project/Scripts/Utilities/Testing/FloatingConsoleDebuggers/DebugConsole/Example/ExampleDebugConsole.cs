using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public class ExampleDebugConsole : MonoBehaviour
    {
        public enum Command
        {
            None,
            Test_DebugConsole,
        }

        public enum MyEnum
        {
            None,
            Something
        }

        public Command _command;

        void Start()
        {
            _command = Command.None;
        }

        private void LateUpdate()
        {
            switch (_command)
            {
                case Command.None:
                    break;
                case Command.Test_DebugConsole:
                    TestDebugConsole();
                    break;
                default:
                    break;
            }
            _command = Command.None;
        }

        private void TestDebugConsole()
        {
            for (int i = 0; i < 15; i++)
            {
                DebugConsole.Instance.__Log("messages 1", LogType.Log, this, "sdsd");
                DebugConsole.Instance.__Log("messages 2", LogType.Error, this, "sdsd");
                DebugConsole.Instance.__Log("messages 3", LogType.Assert, this, "sdsd");
                DebugConsole.Instance.__Log("messages 4", LogType.Exception, this, "sdsd");
                DebugConsole.Instance.__Log("messages 5", LogType.Warning, this, "sdsd");
            }
        }
    }
}
