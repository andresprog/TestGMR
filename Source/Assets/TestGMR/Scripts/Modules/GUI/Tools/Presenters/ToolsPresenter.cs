/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: Tools
 * ScriptType: Presenter
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: Friday, March 13, 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System;

namespace Cofradinn.Modules.Tools
{
    public interface IToolsPresenter : IVisible
    {
        Action<ToolsEventName> _OnPresenterEvent { get; set; }
    }

    public class ToolsPresenter : Presenter, IToolsPresenter
    {
        [Header("Components")]
        [SerializeField] private GameObject _presenter;

        public Action<ToolsEventName> _OnPresenterEvent { get; set; }

        public void __EnableView(bool active)
        {
            _presenter.SetActive(active);
        }
    }
}
