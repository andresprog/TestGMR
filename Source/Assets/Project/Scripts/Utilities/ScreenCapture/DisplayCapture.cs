using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cofradinn.Modules.Utilities
{
    public static class DisplayCapture
    {
        public static void _Capture()
        {
            ScreenCapture.CaptureScreenshot("Capture.png");
        }

    }
}
