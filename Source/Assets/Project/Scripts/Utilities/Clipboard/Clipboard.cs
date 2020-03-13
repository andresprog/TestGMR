using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public static class Clipboard
    {
        public static string _Clipboard
        {
            get { return GUIUtility.systemCopyBuffer; }
            set { GUIUtility.systemCopyBuffer = value; }
        }
    }
}
