/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: TeamMembers
 * ScriptType: Presenter
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Cofradinn.Modules.Gui.TeamMembers
{
    public interface ITeamMembersPresenter : IVisible
    {
        Action<TeamMembersEventName> _OnPanelEvent { get; set; }

        void __EnableInputs(bool enable);
        void __SetTitle(string title);
        void __SetHeaders(List<string> headers);
        void __SetData(List<Dictionary<string, string>> elements);
        void __ClearTableData();
    }

    public class TeamMembersPresenter : Presenter, ITeamMembersPresenter
    {
        [Header("Components")]
        [SerializeField] private GameObject _presenter;
        [SerializeField] private ScrollRect _scrollRectComponent;
        [SerializeField] private Transform _ScrollViewContent;
        [SerializeField] private Text _txtFilesNumber;
        [SerializeField] private Text _txtTitle;
        [SerializeField] private Button _btnRefresh;

        [Header("Assets")]
        [SerializeField] private GameObject _headerPrefab;
        [SerializeField] private GameObject _rowPrefab;

        public Action<TeamMembersEventName> _OnPanelEvent { get; set; }

        private TableHeader _header;
        private List<TableRow> _rows;

        public void __EnableView(bool enable)
        {
            _presenter.SetActive(enable);
        }
        public void __EnableInputs(bool enable)
        {
            _btnRefresh.enabled = enable;
        }

        public void __SetTitle(string title)
        {
            _txtTitle.text = title;
        }
        public void __SetHeaders(List<string> headers)
        {
            __TryInstantiateHeader(headers);
        }
        public void __SetData(List<Dictionary<string, string>> elements)
        {
            if (_header == null) { Debug.LogError("Null Error: _headers is null", this); return; }
            if (_header._headers == null) { Debug.LogError("Header Error: header.count <= 0", this); return; }
            if (_header._headers.Count <= 0) { Debug.LogError("Header Error: header.count <= 0", this); return; }
            if (elements == null) { Debug.LogError("Null Error: elements are null", this); return; }

            __SetElementsNumber(elements.Count);
            _rows = new List<TableRow>();

            if (elements.Count <= 0) return;

            int n = elements.Count;

            foreach (var element in elements)
            {
                if (element != null)
                    __InstantiateRow(element);
                else
                    Debug.LogError("Null Error: This element is Null ");
            }
        }

        public void __ClearTableData()
        {
            _header = null;
            _rows = null;
            int childrens = _ScrollViewContent.transform.childCount;

            if (childrens > 0)
            {
                for (int i = 0; i < childrens; i++)
                {
                    Destroy(_ScrollViewContent.transform.GetChild(childrens - i - 1).gameObject);
                }
            }
        }
        public void __OnclickRefresh()
        {
            _OnPanelEvent?.Invoke(TeamMembersEventName.Refresh);
        }
        public void __OnclickExit()
        {
            _OnPanelEvent?.Invoke(TeamMembersEventName.Exit);
        }

        private void __SetElementsNumber(int elementsCount)
        {
            _txtFilesNumber.text = "Team Members: (" + elementsCount.ToString() + ")";
        }
        private void __TryInstantiateHeader(List<string> headers)
        {
            if (headers == null) { Debug.LogError("Null Error: _headersRow is null", this); return; }

            if (_header == null)
            {
                GameObject obj = Instantiate(_headerPrefab, _ScrollViewContent);
                TableHeader headerObj = obj.GetComponent<TableHeader>();
                headerObj.__CreateHeadersFields(headers);
                _header = headerObj;
            }
            else
            {
                _header.__ClearHeaders();
                _header.__CreateHeadersFields(headers);
            }


        }
        private void __InstantiateRow(Dictionary<string, string> fields)
        {
            if (_header == null) { Debug.LogError("Null Error: _headersRow is null", this); return; }
            if (_header._headers == null) { Debug.LogError("Null Error: _headersRow is null", this); return; }
            if (_header._headers.Count <= 0) { Debug.LogError("Null Error: _headersRow is null", this); return; }

            GameObject obj = Instantiate(_rowPrefab, _ScrollViewContent);
            TableRow row = obj.GetComponent<TableRow>();
            row.__CreateEmptyFields(_header._headers);
            row.__SetFieldsData(fields);
            _rows.Add(row);
        }
    }
}
