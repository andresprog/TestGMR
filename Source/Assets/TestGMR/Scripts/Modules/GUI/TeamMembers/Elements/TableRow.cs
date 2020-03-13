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
    public class TableRow : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _rowContentParent;
        [Header("Assets")]
        [SerializeField] private GameObject _txtFieldPrefab;

        public Dictionary<string, string> _rowData { get; private set; }
        private Dictionary<string, Text> _rowTextComponents { get; set; }

        public void __CreateEmptyFields(List<string> fieldsNames)
        {
            _rowData = new Dictionary<string, string>();
            _rowTextComponents = new Dictionary<string, Text>();

            foreach (var name in fieldsNames)
                __InstantiateField(name);
        }
        private void __InstantiateField(string key)
        {
            if (string.IsNullOrEmpty(key)) { Debug.LogError("Null Error: key is null or empty", this); return; }

            GameObject obj = Instantiate(_txtFieldPrefab, _rowContentParent);
            Text txt = obj.GetComponent<Text>();
            txt.text = "";
            _rowData.Add(key, "");
            _rowTextComponents.Add(key, txt);
        }

        public void __SetFieldsData(Dictionary<string, string> fields)
        {
            if (_rowData == null) { Debug.LogError("Null Error: _rowData is null", this); return; }
            if (fields == null) { Debug.LogError("Null Error: fields is null", this); return; }

            foreach (var field in fields)
            {
                //Text text=null;
                //Debug.LogError("Warning Experimental line", this);

                if (_rowTextComponents.ContainsKey(field.Key))
                {
                    _rowTextComponents.TryGetValue(field.Key, out Text text);
                    _rowData[field.Key] = field.Value;
                    _rowTextComponents[field.Key].text = field.Value;
                }
            }
        }
        public void __ClearRow()
        {
            _rowData = null;
            _rowTextComponents = null;

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