#if UNITY_EDITOR
using Cofradinn.Utilities.IconsReplacer;
#endif
using Cofradinn.Utilities.Editor.Hierarchy;
using UnityEngine;
using Cofradinn.Utilities;

namespace Cofradinn
{
    public abstract class Handler : MonoBehaviour, IIconable
    {
        private Icontype _iconType = Icontype.Handler;
        public Icontype _IconType => _iconType;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            IconsReplacer.__PutMyIcon(this, IconType.Handler);
        }
#endif
    }
}