/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: Feedback
 * ScriptType: Example
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: #CREATIONDATE#
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using Cofradinn.Modules.Feedback;
using UnityEngine;
using HandlerTag = Cofradinn.Constants.HandlerTag;

namespace Cofradinn.Modules.Examples
{
	public class FeedbackExample : Example
	{
		public string _text;
		public FeedbackType FeedbackType;

		#region Commands
		[Header("Commands")]
		[SerializeField] private Command _command = Command.None;

		private void Update()
		{
			switch (_command)
            {
                case Command.SendFeedback:
					FeedbackSystem._Instance.__SendFeedback(_text, FeedbackType);
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
			SendFeedback,
		}
		#endregion
	}
}
