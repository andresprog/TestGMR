/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: DebugPanel
 * ScriptType: Presenter
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: sábado, 15 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Cofradinn.Modules.Feedback;

namespace Cofradinn.Modules.DebugPanel
{
    public interface IDebugPanelPresenter : IVisible
    {
        Action<DebugPanelEventName> _OnPresenterEvent { get; set; }
        List<DebugPanelElement> _CurrentItems { get; }

        void __ClearListView();
        void __BlockScroll(bool block);
        void __SetElementsNumber(int files);
        void __AddAndUpdateOneItemOnPanelList(DebugPanelElementData element);
        void __AddAndUpdateItemsOnPanelList(List<DebugPanelElementData> elements);
    }

    public class DebugPanelPresenter : Presenter, IDebugPanelPresenter
    {
        [Header("Components")]
        [SerializeField] private GameObject _presenter;
        [SerializeField] private ScrollRect _scrollRectComponent;
        [SerializeField] private Transform _ScrollViewContent;
        [SerializeField] private Text _txtFilesNumber;

        [Header("Assets")]
        [SerializeField] private GameObject _itemPrefab;

        public Action<DebugPanelEventName> _OnPresenterEvent { get; set; }
        public List<DebugPanelElement> _CurrentItems { get; private set; }

        public void __EnableView(bool active)
        {
            _presenter.SetActive(active);
        }

        public void __ClearListView()
        {
            int childrens = _ScrollViewContent.transform.childCount;

            if (childrens > 0)
            {
                for (int i = 0; i < childrens; i++)
                {
                    Destroy(_ScrollViewContent.transform.GetChild(childrens - i - 1).gameObject);
                }
            }

            __UpdateCounter();
        }
        public void __BlockScroll(bool block)
        {
            Debug.Log("TODO: add this functionality");
        }
        public void __SetElementsNumber(int files)
        {
            _txtFilesNumber.text = "Elementos: (" + files.ToString() + ")";
        }

        private void __InstantiateItem(int listId, DebugPanelElementData data)
        {
            if (data == null) { Debug.LogError("Null Error: item is null", this); return; }

            GameObject obj = Instantiate(_itemPrefab, _ScrollViewContent);
            DebugPanelElement item = obj.GetComponent<DebugPanelElement>();
            item.__SetData(listId, data);
            _CurrentItems.Add(item);

            __UpdateCounter();
        }
        public void __AddAndUpdateItemsOnPanelList(List<DebugPanelElementData> elements)
        {
            if (elements == null) { Debug.LogError("Null Error: itemList is null", this); return; }
            if (_CurrentItems == null) _CurrentItems = new List<DebugPanelElement>();

            if (elements.Count <= 0) return;

            foreach (var item in elements)
                __AddAndUpdateOneItemOnPanelList(item);
        }
        public void __AddAndUpdateOneItemOnPanelList(DebugPanelElementData element) 
        {
            if (element == null) { Debug.LogError("Null Error: itemList is null", this); return; }

            DebugPanelElement debugPanelElement = _CurrentItems.Find(x => x._Key == element._key);

            if (debugPanelElement == null)
                __InstantiateItem(_CurrentItems.Count, element);
            else
                debugPanelElement.__UpdateData(element);
        }

        public void __UpdateCounter() 
        {
            if (_CurrentItems == null) _CurrentItems = new List<DebugPanelElement>();
            __SetElementsNumber(_CurrentItems.Count);
        }

        private void Start()
        {
            //    FeedbackSystem._Instance.__SendFeedback("Licencia Para Testing Activada!", FeedbackType.Warning);
        }
    }
}
