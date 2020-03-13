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
using UnityEditor;
using UnityEditorInternal;
using Cofradinn.Utilities;
using static Cofradinn.Utilities.HierarchyData;

/// <summary>
/// Scriptable object, for edit data on inspector
/// </summary>
[CustomEditor(typeof(HierarchyData))]
public class HierarchyDataEditor : Editor
{

    private ReorderableList TagList;
    private HierarchyData m_Data;

    void OnEnable()
    {
        TagList = new ReorderableList(serializedObject, serializedObject.FindProperty(HierarchyData._PROPERTY_NAME), true, true, true, true);
        TagList.drawHeaderCallback = OnDrawHeader;
        TagList.drawElementCallback = OnDrawElement;
        TagList.onAddCallback = __OnclickAddElement;
        TagList.onRemoveCallback = __OnclickRemoveElement;
        TagList.onSelectCallback = __OnSelectElement;
        TagList.onChangedCallback = __OnChange;

        if (m_Data == null) return;
        m_Data._apply = true;
    }
    private void __OnclickAddElement(ReorderableList reorderableList)
    {
        if (m_Data == null) return;
        //Debug.Log("Add one");
        m_Data.__AddOne();
    }
    private void __OnclickRemoveElement(ReorderableList reorderableList)
    {
        if (m_Data == null) return;
        // Debug.Log("Delete index");
        if (reorderableList.index >= 0) m_Data.__DeleteIndex(reorderableList.index);
        else m_Data.__DeleteTheLast();
    }
    private void __OnSelectElement(ReorderableList reorderableList)
    {
        int selectedId = reorderableList.index;
        //  Debug.Log("delete selectedId: " + selectedId);
        //  m_Data.__DeleteTheLast();
    }
    private void __OnChange(ReorderableList reorderableList)
    {
        if (m_Data == null) return;
        m_Data._apply = true;
    }
    private void OnDrawHeader(Rect rect)
    {
        rect.x += 15;
        EditorGUI.LabelField(rect, new GUIContent("Tag", ""), EditorStyles.boldLabel);
        rect.x += 125;
        EditorGUI.LabelField(rect, new GUIContent("Icon", ""), EditorStyles.boldLabel);
        rect.x += 150;
        EditorGUI.LabelField(rect, new GUIContent("Color", ""), EditorStyles.boldLabel);
    }
    private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocus)
    {
        if (m_Data == null) return;

        // Initial data
        var element = TagList.serializedProperty.GetArrayElementAtIndex(index);
        string tag = element.FindPropertyRelative("Keyword").stringValue;
        Color tint = element.FindPropertyRelative("TintColor").colorValue;
        rect.y += 2;

        // Keyword
        string name = EditorGUI.TextArea(new Rect(rect.x, rect.y, 125, EditorGUIUtility.singleLineHeight), tag, EditorStyles.textArea);

        //Debug.Log("index: " + index);
        //Debug.Log(" m_Data._Tags.count: " + m_Data._Tags.Count);
        if (m_Data._Tags.Count > index) m_Data._Tags[index] = name;
        if (m_Data._HierarchyTagsIcons.Count > index) m_Data._HierarchyTagsIcons[index].Keyword = name;
        HierarchyTagsIcons hierarchyTagsIcons = null;
        m_Data._FullIDList.TryGetValue(index, out hierarchyTagsIcons);
        if (hierarchyTagsIcons != null) hierarchyTagsIcons.Keyword = name;

        // path
        EditorGUI.PropertyField(new Rect(rect.x + 130, rect.y, 150, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Icon"), GUIContent.none);

        // color
        tint = EditorGUI.ColorField(new Rect(rect.x + 285, rect.y, 100, EditorGUIUtility.singleLineHeight), new GUIContent("", ""), tint);

        element.FindPropertyRelative("Keyword").stringValue = tag;
        element.FindPropertyRelative("TintColor").colorValue = tint;
    }

    public override void OnInspectorGUI()
    {
        m_Data = (HierarchyData)target;
        if (m_Data == null) return;

        serializedObject.Update();
        TagList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        GUILayout.BeginVertical("box");
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        m_Data.ShowIcons = EditorGUILayout.ToggleLeft("Show Icons", m_Data.ShowIcons, EditorStyles.toolbarButton);
        //hd.HorizontalPosition = EditorGUILayout.Slider("Horizontal Position", hd.HorizontalPosition,0,200);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Label("Update Changes", EditorStyles.boldLabel);
        m_Data._apply = EditorGUILayout.ToggleLeft("Apply", m_Data._apply, EditorStyles.toolbarButton);
        //m_Data.HorizontalPosition = EditorGUILayout.Slider("Horizontal Position", m_Data.HorizontalPosition, 0, 200);
        GUILayout.EndVertical();

        //#region Edited by Andrés
        //GUILayout.BeginVertical("box");
        //GUILayout.Label("New Icons", EditorStyles.boldLabel);
        //m_Data.AddIcon = EditorGUILayout.ToggleLeft("Add", m_Data.AddIcon, EditorStyles.toolbarButton);
        //GUILayout.EndVertical();

        //GUILayout.BeginVertical("box");
        //GUILayout.Label("Delete", EditorStyles.boldLabel);
        //m_Data.DeleteSelectedIcon = EditorGUILayout.ToggleLeft("Delete Last", m_Data.DeleteSelectedIcon, EditorStyles.toolbarButton);
        //GUILayout.EndVertical();
        //#endregion



        if (GUI.changed)
        {
            EditorUtility.SetDirty(m_Data);
        }
    }

}
