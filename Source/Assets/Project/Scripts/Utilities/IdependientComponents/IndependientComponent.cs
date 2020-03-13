#if UNITY_EDITOR
using Cofradinn.Utilities.IconsReplacer;
#endif
using UnityEngine;

namespace Cofradinn
{
    public class IndependentComponent : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            IconsReplacer.__PutMyIcon(this, IconType.IndependientComponent);
        }
#endif
    }
}