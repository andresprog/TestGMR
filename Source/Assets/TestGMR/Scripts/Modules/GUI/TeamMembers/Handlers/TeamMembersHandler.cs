/**************************************************************************
 * Module: TeamMembers
 * ScriptType: Handler
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;

namespace Cofradinn.Modules.Gui.TeamMembers
{
	public interface ITeamMembersHandler : IVisible
	{
        Action<TeamMembersEventName> _OnPanelEvent { get; set; }

        void __ClearTableData();
        void __EnableInputs(bool enable);
        void __SetTitle(string title);
        void __SetHeaders(List<string> headers);
        void __SetData(List<Dictionary<string, string>> elements);
    }

	public class TeamMembersHandler : Handler, ITeamMembersHandler
	{
		[Header("Components")]
		[SerializeField] private TeamMembersPresenter _teamMemberPresenter;
		private ITeamMembersPresenter _iTeamMembersPresenter => _teamMemberPresenter;

        public Action<TeamMembersEventName> _OnPanelEvent
        {
            get => _iTeamMembersPresenter._OnPanelEvent;
            set => _iTeamMembersPresenter._OnPanelEvent = value;
        }

        private void Awake()
        {
            __EnableView(false);
        }
        public void __EnableView(bool enable)=> _iTeamMembersPresenter.__EnableView(enable);
        public void __EnableInputs(bool enable)=> _iTeamMembersPresenter.__EnableInputs(enable);

        public void __SetTitle(string title) => _iTeamMembersPresenter.__SetTitle(title);
        public void __SetHeaders(List<string> headers) => _iTeamMembersPresenter.__SetHeaders(headers);
        public void __SetData(List<Dictionary<string, string>> elements) => _iTeamMembersPresenter.__SetData(elements);

        public void __ClearTableData() => _iTeamMembersPresenter.__ClearTableData();
    }
}
