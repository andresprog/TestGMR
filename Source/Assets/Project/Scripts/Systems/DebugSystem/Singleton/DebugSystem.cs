/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: Debug
 * ScriptType: System
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: sábado, 15 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using Cofradinn.Modules.DebugPanel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cofradinn.Modules.DebugFloatingPanel
{
    public class DebugSystem : SingletonComponent<DebugSystem>
    {
        [Header("Components")]
        [SerializeField] private DebugPanelHandler _debugPanelHandler;

        private IDebugPanelHandler _iDebugPanelHandler => _debugPanelHandler;

        public void __DebugData(List<DebugPanelElementData> elements)
        {
            _iDebugPanelHandler.__EnableView(true);
            _iDebugPanelHandler.__AddOrUpdateElements(elements);
        }
        public void __DebugData(params DebugPanelElementData[] elements)
        {
            _iDebugPanelHandler.__EnableView(true);
#if UNITY_ANDROID
            Debug.LogWarning("You should check if (using System.Linq) works on android!");
#endif
            _iDebugPanelHandler.__AddOrUpdateElements(elements.ToList());
        }
        public void __DebugData(DebugPanelElementData element)
        {
            _iDebugPanelHandler.__EnableView(true);
            _iDebugPanelHandler.__AddOrUpdateElement(element);
        }

        protected override void OnAwake()
        {
            //  throw new System.NotImplementedException();
        }
    }
}
