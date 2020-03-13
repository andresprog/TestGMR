/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: TeamMebers
 * ScriptType: Element
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Modules.Gui.TeamMembers
{
    public class TableHeader : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _rowContentParent;
        [Header("Assets")]
        [SerializeField] private GameObject _txtPrefab;

        public List<string> _headers;
        private Dictionary<string, Text> _headersTextComponents;

        public void __CreateHeadersFields(List<string> headers)
        {
            _headers = new List<string>();
            _headersTextComponents = new Dictionary<string, Text>();

            foreach (var header in headers)
                __InstantiateHaeaderField(header);
        }
        private void __InstantiateHaeaderField(string key)
        {
            if (string.IsNullOrEmpty(key)) { Debug.LogError("Null Error: key is null or empty", this); return; }

            GameObject obj = Instantiate(_txtPrefab, _rowContentParent);
            Text txt = obj.GetComponent<Text>();
            txt.text = key;
            _headers.Add(key);
            _headersTextComponents.Add(key, txt);
        }

        public void __ClearHeaders()
        {
            _headers = null;
            _headersTextComponents = null;

            int childrens = _rowContentParent.transform.childCount;

            if (childrens > 0)
            {
                for (int i = 0; i < childrens; i++)
                {
                    Destroy(_rowContentParent.transform.GetChild(childrens - i - 1).gameObject);
                }
            }
        }
    }
}