/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: DebugPanel
 * ScriptType: Handler
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: sábado, 15 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;

namespace Cofradinn.Modules.DebugPanel
{
	public interface IDebugPanelObservable
	{
		// In this interface you should put the get methods and get properties
	}

	public interface IDebugPanelHandler : IDebugPanelObservable, IVisible
	{
        Action<DebugPanelEventName> _OnModuleEvent { get; set; }
        void __AddOrUpdateElement(DebugPanelElementData element);
        void __AddOrUpdateElements(List<DebugPanelElementData> elements);
    }

	public class DebugPanelHandler : Handler, IDebugPanelHandler
	{
		[Header("Components")]
		[SerializeField] private DebugPanelPresenter _DebugPanelPresenter;
		private IDebugPanelPresenter _iDebugPanelPresenter => _DebugPanelPresenter;

        public Action<DebugPanelEventName> _OnModuleEvent
        {
            get => _iDebugPanelPresenter._OnPresenterEvent;
            set => _iDebugPanelPresenter._OnPresenterEvent = value;
        }

        public void __EnableView(bool enable)
		{
			_iDebugPanelPresenter.__EnableView(enable);
		}
        public void __AddOrUpdateElements(List<DebugPanelElementData> files)
        {
            _iDebugPanelPresenter.__AddAndUpdateItemsOnPanelList(files);
        }
        public void __AddOrUpdateElement(DebugPanelElementData element)
        {
            _iDebugPanelPresenter.__AddAndUpdateOneItemOnPanelList(element);
        }
        private void Awake()
        {
            __EnableView(true);
            _iDebugPanelPresenter.__ClearListView();
            __EnableView(false);
        }
    }
}
