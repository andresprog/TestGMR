using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public class ExampleDebugVariable : MonoBehaviour
    {
        public enum MyEnum
        {
            None,
            Something,
            OtherValue,
        }

        public int num;
        public string name;
        public MyEnum enumExample;

        private void LateUpdate()
        {
            TestDebugVariable();
        }

        void TestDebugVariable()
        {
            DebugVariables.Instance.__Log("messages 1: " + name, LogType.Log, this);
            DebugVariables.Instance.__Log("messages 2: " + num.ToString(), LogType.Error, this);
            DebugVariables.Instance.__Log("messages 3: " + enumExample.ToString(), LogType.Assert, this);
            DebugVariables.Instance.__Log("jose: " + name, LogType.Error, this);
        }
    }
}
