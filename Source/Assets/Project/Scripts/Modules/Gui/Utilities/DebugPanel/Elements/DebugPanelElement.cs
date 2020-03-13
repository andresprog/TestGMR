/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: DebugPanel
 * ScriptType: Element
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Modules.DebugPanel
{
    public class DebugPanelElement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Text _txtKey;
        [SerializeField] private Text _txtData;

        private DebugPanelElementData _debugPanelElementData;
        public string _Key { get => _txtKey.text; }
        public string _Data { get => _txtData.text; }
        public int _Id { get; private set; }

        public void __SetData(int id, DebugPanelElementData fileInfo)
        {
            if (fileInfo == null) { Debug.LogError("Null Error"); return; }

            _Id = id;
            _debugPanelElementData = fileInfo;
            _txtKey.text = fileInfo._key;
            _txtData.text = fileInfo._data;
        }
        public void __UpdateData(DebugPanelElementData fileInfo)
        {
            if (fileInfo == null) { Debug.LogError("Null Error"); return; }

            _debugPanelElementData = fileInfo;
            _txtKey.text = fileInfo._key;
            _txtData.text = fileInfo._data;
        }
        public void __SetData(DebugPanelElement debugPanelElement)
        {
            if (debugPanelElement == null) { Debug.LogError("Null Error"); return; }

            _debugPanelElementData = new DebugPanelElementData(debugPanelElement._Key, debugPanelElement._Data);
            __SetData(debugPanelElement._Id, _debugPanelElementData);
        }
    }
}

/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: DebugPanelElement
 * ScriptType: Data
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: Friday, January 17, 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

//using System;
//using UnityEngine;

namespace Cofradinn.Modules.DebugPanel
{
    [System.Serializable]
    public class DebugPanelElementData
    {
        public string _key;
        public string _data;

        public DebugPanelElementData() { }
        public DebugPanelElementData(string key, string data)
        {
            _key = key;
            _data = data;
        }
    }
}