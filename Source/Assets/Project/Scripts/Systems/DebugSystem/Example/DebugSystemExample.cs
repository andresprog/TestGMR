/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: DebugPanel
 * ScriptType: Example
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: #CREATIONDATE#
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using Cofradinn.Modules.DebugFloatingPanel;
using System.Collections.Generic;
using UnityEngine;
using HandlerTag = Cofradinn.Constants.HandlerTag;

namespace Cofradinn.Modules.DebugPanel
{
    public class DebugSystemExample : Example
    {
        [Header("Commands")]
        [SerializeField] private Command _command = Command.None;
        [Header("Test Element")]
        [SerializeField] private DebugPanelElementData _element;
        [Header("Test Elements")]
        [SerializeField] private List<DebugPanelElementData> _elements;

        #region Commands
        private void __PutElement()
        {
            DebugSystem._Instance.__DebugData(_element);
        }

        private void __PutElements()
        {
            DebugSystem._Instance.__DebugData(_elements);
        }

        private void Update()
        {
            switch (_command)
            {
                case Command.PutElement:
                    __PutElement();
                    break;
                
                case Command.PutElements:
                    __PutElements();
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
            PutElement,
            PutElements,
        }
        #endregion
    }
}
