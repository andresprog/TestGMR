using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This editor allows to have a variable height for comments in the editor.
/// </summary>
[CustomEditor(typeof(Comment))]
public class CommentEditor : Editor
{
    SerializedProperty commentProperty;
    SerializedProperty commentHeightProperty;

    private void OnEnable()
    {
        commentProperty = serializedObject.FindProperty("comment");
        commentHeightProperty = serializedObject.FindProperty("commentHeight");
    }

    public override void OnInspectorGUI()
    {
        commentHeightProperty.intValue = EditorGUILayout.IntSlider(commentHeightProperty.intValue, 20, 200);

        commentProperty.stringValue = EditorGUILayout.TextArea(commentProperty.stringValue, GUILayout.Height(commentHeightProperty.intValue));

        serializedObject.ApplyModifiedProperties();
    }
}
