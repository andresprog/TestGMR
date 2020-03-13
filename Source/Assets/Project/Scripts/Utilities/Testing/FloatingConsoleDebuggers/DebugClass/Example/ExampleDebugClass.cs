using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public class ExampleDebugClass : MonoBehaviour
    {
        [SerializeField] private TestDebugClass _TestDebugClass;

        private void LateUpdate()
        {
            DebugClass._Instance.__Log2(_TestDebugClass);
        }

        [System.Serializable]
        public class TestDebugClass
        {
            private int other { get; set; }
            public int Num
            {
                get
                {
                    return num;
                }
            }


            public int num;
            public string name;
            public MyEnum enumExample;
            private MyEnum privateEnum;
            protected MyEnum protect;
        }
        public enum MyEnum
        {
            None,
            Something,
            OtherValue,
        }
    }
}