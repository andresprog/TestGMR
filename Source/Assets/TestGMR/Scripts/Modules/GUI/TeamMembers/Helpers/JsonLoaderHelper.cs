/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: TeamMembers
 * ScriptType: Helper
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System.IO;
using Cofradinn.Modules.Feedback;

namespace Cofradinn.Modules.Gui.TeamMembers
{
    public static class JsonLoaderHelper
    {
        public static string __LoadJsonFile(string fileName)
        {
            string path = Path.Combine(Application.streamingAssetsPath, fileName);

            if (!File.Exists(path))
            {
                Debug.LogError("Null Error: Not found this file " + path);
                FeedbackSystem._Instance.__SendFeedback("path not found: " + path, FeedbackType.Error);
                return default;
            }

            return File.ReadAllText(path);
        }
    }
}
