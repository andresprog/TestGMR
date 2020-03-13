using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities.Docs
{
    public class DocumentationNote : MonoBehaviour
    {
        [SerializeField] private Note _note;

        private void Start()
        {
            Debug.LogError("Note: Select this object", this);
        }
    }

    [System.Serializable]
    public class Note
    {
        [SerializeField] private string _title;
        [Multiline(5)]
        [SerializeField] private string _note;
    }
}