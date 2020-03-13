/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: Tools
 * ScriptType: Example
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: #CREATIONDATE#
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using HandlerTag = Cofradinn.Constants.HandlerTag;

namespace Cofradinn.Modules.Tools
{
	public class ToolsExample : Example
	{
		[Header("Components")]
		private IToolsHandler _iToolsHandler;
    
		protected override void __FindReferences()
        {
		    // Finding modules references
            _iToolsHandler = __FindComponent<IToolsHandler>(HandlerTag.ToolsHandler);
        }

		#region Commands
		[Header("Commands")]
		[SerializeField] private Command _command = Command.None;

		private void Update()
		{
			switch (_command)
            {
                case Command.Command_1:
					Debug.Log("Execute Command_1");
                    break;
                case Command.Command_2:
					Debug.Log("Execute Command_2");
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
			Command_1,
			Command_2,
		}
		#endregion
	}
}
