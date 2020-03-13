using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple comment useful to document game objects in the editor.
/// </summary>
public class Comment : MonoBehaviour
{
#if UNITY_EDITOR
    public string comment;

    /// <summary>
    /// used to allocate enough space to display the comment completely.
    /// </summary>
    public int commentHeight;

    private void Awake()
    {
        Destroy(this);
    }
#endif
}
