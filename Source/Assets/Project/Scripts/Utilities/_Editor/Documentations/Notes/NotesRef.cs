#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities.Documentation
{
    public class NotesRef : MonoBehaviour
    {
        [SerializeField] private Note[] _notes;

        private void Start()
        {
            Debug.LogError("Note: Select this object", this);
        }
    }
}
#endif