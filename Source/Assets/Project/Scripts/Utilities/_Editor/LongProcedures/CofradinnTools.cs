using Cofradinn.Utilities.ScriptTemplates;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace Cofradinn.Editor.Utilities
{
    public class CofradinnTools : MonoBehaviour
    {
        private const string _BASE_PATH = "Cofradinn/MenuExamples/";
        #region Examples

        /// <summary>
        /// Metodo que aparece en el menu superior del editor
        /// </summary>
        [MenuItem(_BASE_PATH + "Example Debugs %#&a")]
        static void DebugLogName()
        {
            foreach (Transform transform in Selection.transforms)
            {
                Debug.Log("selected object: " + transform.name);
            }
        }
        [MenuItem(_BASE_PATH + "Example2 Debugs %#&d")]
        static void DebugLogName3()
        {
            foreach (Transform transform in Selection.transforms)
            {
                Debug.Log("selected object: " + transform.name + "3");
            }
        }

        /// <summary>
        /// Metodo que aparece en el menu superior del editor
        /// </summary>
        [MenuItem(_BASE_PATH + "Example Extract Component")]
        static void ExtractComponent(MenuCommand menuCommand)
        {
            // The RigidBody component can be extracted from the menu command using the context field.
            var obj = menuCommand.context as Rigidbody;
            //var rigid = obj.GetComponent<Rigidbody>();

            Debug.Log("obj: " + menuCommand.userData);
            obj.useGravity = false;
        }

        /// <summary>
        /// Darle click secundario a una camara para que aparezca la opcion de ejecutar el metodo
        /// </summary>
        [MenuItem("CONTEXT/Camera/DoCameraThing")]
        private static void DoCameraThing(MenuCommand cmd)
        {
            Camera cam = cmd.context as Camera;
            Debug.Log("obj: " + cam.name);
        }

        #region On Inspector
        // MonoBehaviour
        [ContextMenu("ContextSomething")]
        private void ContentSomething()
        {
            // Execute some code
        }


        [ContextMenuItem("Reset", "ResetDate")]
        [ContextMenuItem("Set to Now", "SetDateToNow")]
        public string Date = "";

        public void ResetDate()
        {
            Date = "";
        }

        public void SetDateToNow()
        {
            Date = DateTime.Now.ToString();
        }
        #endregion
        #endregion
    }
}
#endif