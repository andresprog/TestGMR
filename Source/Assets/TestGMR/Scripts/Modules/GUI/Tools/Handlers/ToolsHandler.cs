/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: Tools
 * ScriptType: Handler
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: Friday, March 13, 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System;

namespace Cofradinn.Modules.Tools
{
	public interface IToolsObservable
	{
		// In this interface you should put the get methods and get properties
	}

	public interface IToolsHandler : IToolsObservable, IVisible
	{
		// Here will be all the methods and properties for the administractors
		Action<ToolsEventName> _OnModuleEvent { get; set; }
	}

	public class ToolsHandler : Handler, IToolsHandler
	{
		[Header("Components")]
		[SerializeField] private ToolsPresenter _ToolsPresenter;
		private IToolsPresenter _iToolsPresenter => _ToolsPresenter;
		
		public Action<ToolsEventName> _OnModuleEvent { get; set; }

		public void __EnableView(bool enable)
		{
			_iToolsPresenter.__EnableView(enable);
		}
	}
}
