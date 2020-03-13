/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: Hierarchy
 * ScriptType: Editor
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: Only Date: Sunday, January 5, 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Cofradinn.Utilities;

/// <summary>
/// Hierarchy Drawer
/// </summary>
[InitializeOnLoad]
public class HierarchyEditor
{
    private static List<int> sceneObjects;
    private static HierarchyData m_Data;

    private static float _posHor = 0;
    private static int _id = 0;

    static HierarchyEditor()
    {
        m_Data = Resources.Load(HierarchyData._SCRIPTABLE_OBJECT_INSTANCE_NAME, typeof(HierarchyData)) as HierarchyData;
        EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchy;
        EditorApplication.projectChanged += UpdateObjects;
        EditorApplication.hierarchyChanged += UpdateObjects;
        EditorApplication.playModeStateChanged += UpdateObjects;
        EditorApplication.update += UpdateObjects;
    }

    private static void UpdateObjects(PlayModeStateChange playModeStateChange)
    {
        UpdateObjects();
    }
    private static void UpdateObjects()
    {
        if (m_Data == null)
        {
            m_Data = Resources.Load(HierarchyData._SCRIPTABLE_OBJECT_INSTANCE_NAME, typeof(HierarchyData)) as HierarchyData;
            if (m_Data == null) return;
        }

        if (m_Data._apply)
        {
            m_Data._apply = false;

            // refresh tag list
            m_Data.RefreshTags();

            //get all objects in hierarchy (active objects).
            GameObject[] go = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            for (int i = 0; i < go.Length; i++)
            {
                m_Data.RegisterObject(go[i]);
            }
        }
    }
    static void DrawHierarchy(int instanceID, Rect selectionRect)
    {
        if (m_Data == null)
            return;
        if (m_Data.isNull)
        {
            UpdateObjects();
            return;
        }

        if (!m_Data.ShowIcons)
            return;

        UpdateObjects();

        // place the icon to the right of the list:
        Rect r = new Rect(selectionRect);

        //Debug.Log("r.w: " + r.width);
        //Debug.Log("r.x: " + r.x);
        if (instanceID != 0)
            if (r.width >= 2)
            {
                _id = instanceID;
                _posHor = r.width;
            }

        if (_id == instanceID)
        {
            _id = instanceID;
            _posHor = r.width;
        }

        r.x = 14;
        r.width = 20;

        //if this object is in the list of tags with icon
        if (m_Data.ContainID(instanceID))
        {
            HierarchyData.HierarchyTagsIcons t = m_Data.GetTagInfo(instanceID);
            if (t != null && t.Icon != null)
            {
                GUI.color = t.TintColor;
                GUI.Label(r, new GUIContent(t.Icon, t.Keyword));
                GUI.color = Color.white;
            }
        }
    }
}
