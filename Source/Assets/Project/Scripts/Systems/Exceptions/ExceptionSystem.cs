using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Systems
{
    public class ExceptionSystem : MonoBehaviour
    {
        void __Examples()
        {
            throw new System.Exception();
            throw new System.FieldAccessException();
            throw new System.ArgumentException();
        }
    }
}
