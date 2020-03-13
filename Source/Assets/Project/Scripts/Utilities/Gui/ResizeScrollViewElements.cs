using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Utilities
{
    public class ResizeScrollViewElements : MonoBehaviour
    {
        [Tooltip("Put a combination of the values where both are compatible in size")]
        [Header("Good Sizes")]
        [SerializeField] private float _screenWidth;
        [SerializeField] private float _widthCeld;
        [Header("Delta Values, Read Only")]
        [SerializeField] private float _deltaWidth;

        private void Awake()
        {
            _deltaWidth = _screenWidth / _widthCeld;
            float currenttWidthT = Screen.width / _deltaWidth;
            Vector2 newSize = new Vector2(currenttWidthT, this.gameObject.GetComponent<GridLayoutGroup>().cellSize.y);
            this.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!EditorApplication.isPlaying)
            {
                _deltaWidth = _screenWidth / _widthCeld;
            }
        }
#endif


    }
}
