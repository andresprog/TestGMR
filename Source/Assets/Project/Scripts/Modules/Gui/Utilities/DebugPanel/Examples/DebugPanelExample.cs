/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: DebugPanel
 * ScriptType: Example
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: #CREATIONDATE#
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using HandlerTag = Cofradinn.Constants.HandlerTag;

namespace Cofradinn.Modules.DebugPanel
{
    public class DebugPanelExample : Example
    {
        [Header("Commands")]
        [SerializeField] private Command _command = Command.None;
        [Header("Components")]
        [SerializeField] private DebugPanelHandler _debugPanelHandler;
        [Header("Test Parameters")]
        [SerializeField] private List<DebugPanelElementData> _list;

        private IDebugPanelHandler _iDebugPanelHandler => _debugPanelHandler;
        #region Commands
        private void __PutData()
        {
            _iDebugPanelHandler.__EnableView(true);
            _iDebugPanelHandler.__AddOrUpdateElements(_list);
        }

        private void Update()
        {
            switch (_command)
            {
                case Command.ShowData:
                    __PutData();
                    break;

                case Command.None:
                default:
                    break;
            }
            _command = Command.None;
        }

        public enum Command
        {
            None,
            ShowData,
        }
        #endregion
    }
}
