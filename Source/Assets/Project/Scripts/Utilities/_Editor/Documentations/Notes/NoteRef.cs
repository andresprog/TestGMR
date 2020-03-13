#if UNITY_EDITOR
using Cofradinn.Utilities;
using Cofradinn.Utilities.Editor.Hierarchy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities.Documentation
{
    public class NoteRef : MonoBehaviour, IIconable
    {
        [SerializeField] private Note _note;

        [SerializeField] private Icontype _iconType;
        public Icontype _IconType => _iconType;
    }

    [System.Serializable]
    public class Note
    {
        [SerializeField] private string _title;
        [Multiline(5)]
        [SerializeField] private string _note;
        [SerializeField] private GameObject _ref;
    }
}
#endif