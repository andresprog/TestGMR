/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: HierarchyWindow
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
/// Windows in editor for edit data
/// </summary>
public class HierarchyWindowEditor : EditorWindow
{
    static HierarchyData m_Data;
    private ReorderableList TagList;
    private SerializedObject DataSerialized;

    private string _flagName;
    private int _deleteId;



    void OnEnable()
    {
        // Load data
        m_Data = Resources.Load(HierarchyData._SCRIPTABLE_OBJECT_INSTANCE_NAME, typeof(HierarchyData)) as HierarchyData;

        if (m_Data != null)
        {
            DataSerialized = new SerializedObject(m_Data);
            TagList = new ReorderableList(DataSerialized, DataSerialized.FindProperty(HierarchyData._PROPERTY_NAME), true, true, true, true);
            TagList.drawHeaderCallback = OnDrawHeader;
            TagList.drawElementCallback = OnDrawElement;
            TagList.onAddCallback = __OnclickAddElement;
            TagList.onRemoveCallback = __OnclickRemoveElement;
            TagList.onSelectCallback = __OnSelectElement;
            TagList.onChangedCallback = __OnChange;
            m_Data._apply = true;
        }
    }

    private void OnGUI()
    {
        if (m_Data != null)
        {
            DataSerialized.Update();
            TagList.DoLayoutList();
            DataSerialized.ApplyModifiedProperties();

            GUILayout.BeginVertical("box");
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            m_Data.ShowIcons = EditorGUILayout.ToggleLeft("Show Icons", m_Data.ShowIcons, EditorStyles.toolbarButton);
            //m_Data.HorizontalPosition = EditorGUILayout.Slider("Horizontal Position", m_Data.HorizontalPosition, 0, 200);
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUILayout.Label("Update Changes", EditorStyles.boldLabel);
            m_Data._apply = EditorGUILayout.ToggleLeft("Apply", m_Data.ShowIcons, EditorStyles.toolbarButton);
            //m_Data.HorizontalPosition = EditorGUILayout.Slider("Horizontal Position", m_Data.HorizontalPosition, 0, 200);
            GUILayout.EndVertical();

            //GUILayout.BeginVertical("box");
            //GUILayout.Label("New Icons", EditorStyles.boldLabel);
            //m_Data.AddIcon = EditorGUILayout.ToggleLeft("Add", m_Data.AddIcon, EditorStyles.toolbarButton);
            //GUILayout.EndVertical();

            //GUILayout.BeginVertical("box");
            //GUILayout.Label("Delete", EditorStyles.boldLabel);
            //m_Data.DeleteSelectedIcon = EditorGUILayout.ToggleLeft("Delete Last", m_Data.DeleteSelectedIcon, EditorStyles.toolbarButton);
            //GUILayout.EndVertical();

            // Examples
            //var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            //GUILayout.BeginVertical("box");
            //GUILayout.Label("Delete", EditorStyles.boldLabel);
            //GUILayout.Toggle(true, "Delete", EditorStyles.toggle);
            //GUILayout.Toolbar(0, new string[] { "a","b", "c"}, EditorStyles.toolbar);
            //GUILayout.Button(texture, EditorStyles.toolbarButton);
            //GUILayout.Button(texture, EditorStyles.miniButtonLeft);
            //GUILayout.Button(texture, EditorStyles.miniButtonMid);
            //GUILayout.Button(texture, EditorStyles.miniButtonRight);
            //GUILayout.Button(texture, EditorStyles.radioButton);
            //GUILayout.Space(5);
            //GUILayout.RepeatButton(texture, EditorStyles.toolbarButton);
            //GUILayout.RepeatButton(texture, EditorStyles.miniButtonLeft);
            //GUILayout.RepeatButton(texture, EditorStyles.miniButtonMid);
            //GUILayout.RepeatButton(texture, EditorStyles.miniButtonRight);
            //GUILayout.RepeatButton(texture, EditorStyles.radioButton);
            //GUILayout.Space(2);
            //GUILayout.HorizontalSlider(2,0,100);
            //GUILayout.HorizontalScrollbar(2,2,2,2);
            //GUILayout.TextField("text", EditorStyles.textField);
            //GUILayout.TextArea("text", EditorStyles.textField);
            //GUILayout.EndVertical();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(m_Data);
            }
        }
        else
        {
            GUILayout.Label("Data not found.", EditorStyles.boldLabel);
        }

    }
    private void __OnclickAddElement(ReorderableList reorderableList)
    {
        Debug.Log("Add one window");
        m_Data.__AddOne();
    }
    private void __OnclickRemoveElement(ReorderableList reorderableList)
    {
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
        if (m_Data != null) m_Data._apply = true;
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
        var element = TagList.serializedProperty.GetArrayElementAtIndex(index);
        string tag = element.FindPropertyRelative("Keyword").stringValue;
        Color tint = element.FindPropertyRelative("TintColor").colorValue;
        rect.y += 2;

        // keword
        _flagName = EditorGUI.TextArea(new Rect(rect.x, rect.y, 125, EditorGUIUtility.singleLineHeight), tag, EditorStyles.textArea);
        if (m_Data._Tags.Count > index) m_Data._Tags[index] = _flagName;
        if (m_Data._HierarchyTagsIcons.Count > index) m_Data._HierarchyTagsIcons[index].Keyword = _flagName;
        HierarchyTagsIcons hierarchyTagsIcons = null;
        m_Data._FullIDList.TryGetValue(index, out hierarchyTagsIcons);
        if (hierarchyTagsIcons != null) hierarchyTagsIcons.Keyword = _flagName;

        // path
        EditorGUI.PropertyField(new Rect(rect.x + 130, rect.y, 150, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Icon"), GUIContent.none);

        // color
        tint = EditorGUI.ColorField(new Rect(rect.x + 285, rect.y, 100, EditorGUIUtility.singleLineHeight), new GUIContent("", ""), tint);


        element.FindPropertyRelative("Keyword").stringValue = tag;
        element.FindPropertyRelative("TintColor").colorValue = tint;
    }



    [MenuItem("Cofradinn/Hierarchy Icons")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindowWithRect(typeof(HierarchyWindowEditor), new Rect(300, 300, 450, 700), true, "Hierarchy Icons");
    }
}
