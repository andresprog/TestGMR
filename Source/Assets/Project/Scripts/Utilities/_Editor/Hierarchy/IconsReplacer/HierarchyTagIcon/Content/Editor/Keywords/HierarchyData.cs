/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: HierarchyData
 * ScriptType: Editor
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: Only Date: Sunday, January 5, 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using Cofradinn.Utilities.Editor.Hierarchy;

namespace Cofradinn.Utilities
{
    /// <summary>
    /// ScriptableObject
    /// </summary>
    public class HierarchyData : ScriptableObject
    {
        [Header("Tags")]
        public List<HierarchyTagsIcons> _HierarchyTagsIcons = new List<HierarchyTagsIcons>();
        [Header("Settings")]
        public bool ShowIcons = true;
        public bool _apply = false;
        public int _deleteIndex;
        public bool AddIcon = false;
        public bool DeleteSelectedIcon = false;
        [Range(0, 200)]
        public float HorizontalPosition = 0;
        public const string _SCRIPTABLE_OBJECT_INSTANCE_NAME = "HierarchyData";
        public const string _PROPERTY_NAME = "_HierarchyTagsIcons";

        [HideInInspector] public List<string> _Tags = new List<string>();
        [HideInInspector] public Dictionary<int, HierarchyTagsIcons> _FullIDList = new Dictionary<int, HierarchyTagsIcons>();

        /// <summary>
        /// 
        /// </summary>
        public void RefreshTags()
        {
            ClearAll();

            // Clear lists
            // __CleanData();

            if (_Tags.Count > 0)
                for (int i = 0; i < _Tags.Count; i++)
                {
                    if (!ContainsTag(_Tags[i]) || _Tags[i] == "")
                    {
                        if (_Tags[i] == "") { _Tags[i] = "Empty"; Debug.Log("is empty"); }
                        HierarchyTagsIcons hti = new HierarchyTagsIcons();
                        //Debug.Log("hti.Tag: " + hti.Tag);
                        hti.Keyword = _Tags[i];
                        _HierarchyTagsIcons.Add(hti);
                    }
                }
        }
        private void __CleanData()
        {
            _Tags.Clear();
            _HierarchyTagsIcons.Clear();
            _FullIDList.Clear();
        }
        public void __AddOne()
        {
            _Tags.Add("");
            _FullIDList.Add(_FullIDList.Count, new HierarchyTagsIcons());
            _apply = true;
        }
        public void __DeleteTheLast()
        {
            //Debug.Log("FullIDList.Count: " + FullIDList.Count);
            //Debug.Log("_Tags.Count: " + _Tags.Count);
            if (_Tags.Count > 0) _Tags.RemoveAt(_Tags.Count - 1);
            if (_FullIDList.Count > 0) _FullIDList.Remove(_FullIDList.Count - 1);
            if (_HierarchyTagsIcons.Count > 0) _HierarchyTagsIcons.RemoveAt(_HierarchyTagsIcons.Count - 1);
            _apply = true;
        }
        public void __DeleteIndex(int index)
        {
            //Debug.Log("FullIDList.Count: " + FullIDList.Count);
            //Debug.Log("_Tags.Count: " + _Tags.Count);
            // Debug.Log("_deleteIndex: " + _deleteIndex);
            if (_Tags.Count > index) _Tags.RemoveAt(index);
            if (_FullIDList.Count > index) _FullIDList.Remove(index);
            if (_HierarchyTagsIcons.Count > index) _HierarchyTagsIcons.RemoveAt(index);
            _deleteIndex = -1;
            _apply = true;
        }

        //// If contains keyword on the name
        //public void RegisterObject(GameObject go)
        //{
        //    if (go == null) return;
        //    if (go.name == null) return;
        //    if (_HierarchyTagsIcons == null) return;

        //    for (int i = 0; i < _HierarchyTagsIcons.Count; i++)
        //    {
        //        if (_HierarchyTagsIcons[i] == null) continue;

        //        // if object name do not contains the keyword
        //        if (go.name.Contains(_HierarchyTagsIcons[i].Keyword))
        //        {
        //            int id = go.GetInstanceID();
        //            if (!_HierarchyTagsIcons[i].IDs.ContainsKey(id))
        //            {
        //                // add to dictionary
        //                _HierarchyTagsIcons[i].IDs.Add(id, CreateInfo(go));
        //                _FullIDList.Add(id, _HierarchyTagsIcons[i]);
        //            }
        //        }
        //    }
        //}
        // If contains a component with keyword
        public void RegisterObject(GameObject go)
        {
            if (go == null) return;
            if (go.name == null) return;
            if (_HierarchyTagsIcons == null) return;
            IIconable icon = go.GetComponent<IIconable>();

            for (int i = 0; i < _HierarchyTagsIcons.Count; i++)
            {
                if (_HierarchyTagsIcons[i] == null) continue;

                if (icon != null)
                {
                    if (icon._IconType.ToString() == _HierarchyTagsIcons[i].Keyword)
                    {
                        int id = go.GetInstanceID();
                        if (!_HierarchyTagsIcons[i].IDs.ContainsKey(id))
                        {
                            // add to dictionary
                            _HierarchyTagsIcons[i].IDs.Add(id, CreateInfo(go));
                            _FullIDList.Add(id, _HierarchyTagsIcons[i]);
                        }
                    }
                }
            }
        }
        private HierarchyObjectInfo CreateInfo(GameObject go)
        {
            HierarchyObjectInfo hoi = new HierarchyObjectInfo();
            hoi.Name = go.name;
            return hoi;
        }
        public bool ContainID(int id)
        {
            return _FullIDList.ContainsKey(id);
        }
        public bool isNull
        {
            get
            {
                return (_HierarchyTagsIcons == null || _HierarchyTagsIcons.Count <= 0);
            }
        }
        private void ClearAll()
        {
            //_Tags.Clear();
            _FullIDList.Clear();
            for (int i = 0; i < _HierarchyTagsIcons.Count; i++)
            {
                _HierarchyTagsIcons[i].IDs.Clear();
            }
        }
        public HierarchyTagsIcons GetTagInfo(int id)
        {
            return _FullIDList[id];
        }
        private bool ContainsTag(string tag)
        {
            if (_HierarchyTagsIcons.Count <= 0) { return false; }
            for (int i = 0; i < _HierarchyTagsIcons.Count; i++)
            {
                if (_HierarchyTagsIcons[i].Keyword == tag)
                {
                    return true;
                }
            }
            return false;
        }

        [System.Serializable]
        public class HierarchyTagsIcons
        {
            public string Keyword;
            public Texture2D Icon;
            public Color TintColor = Color.white;
            public Dictionary<int, HierarchyObjectInfo> IDs = new Dictionary<int, HierarchyObjectInfo>();
        }

        [System.Serializable]
        public class HierarchyObjectInfo
        {
            public string Name;
            public int VertCount;
        }
    }
}