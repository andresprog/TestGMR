#if UNITY_EDITOR
using Cofradinn.Utilities.IconsReplacer;
#endif
using Cofradinn.Utilities.Editor.Hierarchy;
using UnityEngine;
using Cofradinn.Utilities;

namespace Cofradinn
{
    public abstract class Controller : MonoBehaviour, IIconable
    {
        private Icontype _iconType = Icontype.Controller;
        public Icontype _IconType => _iconType;


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            IconsReplacer.__PutMyIcon(this, IconType.Controller);
        }
#endif

        private void Awake()
        {
            __FindReferences();
            __OnAwake();
        }
        protected abstract void __FindReferences();
        protected virtual void __OnAwake() { }

        protected M __FindComponent<M>(string tag) where M : class
        {
            M comp = null;
            GameObject obj = GameObject.FindGameObjectWithTag(tag);
            if (obj == null) { Debug.LogError("Null Error: GagmeObject " + tag + " is null (Iam: "+ this.name + ")", this); }
            else { comp = obj.GetComponent<M>(); }

            if (comp == null) { Debug.LogError("Null Error: Component " + tag + "  is null", this); return null; }
            else return comp;
        }
        protected GameObject __FindGameObject(string tag)
        {
            GameObject obj = GameObject.FindGameObjectWithTag(tag) ;
            if (obj == null) { Debug.LogError("Null Error: GagmeObject " + tag + " is null", this); return null; }
            else { return obj; }
        }
    }
}