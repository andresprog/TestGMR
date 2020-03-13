using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace Cofradinn.Modules.Utilities
{
    /// <summary>
    /// Class for custom debugs
    /// </summary>
    public static class CustomDebug
    {
        /// <summary>
        /// show all the fields of the object
        /// </summary>
        /// <typeparam name="T">object type: class</typeparam>
        /// <param name="obj"></param>
        public static void __DebugAllFields<T>(T obj) where T : class
        {
            if (obj == null) { Debug.LogError("Null"); return; }
            if (obj.GetType().GetFields() == null) { Debug.LogError("Null"); return; }

            foreach (var field in obj.GetType().GetFields())
            {
                Debug.Log("field.Name: " + field.Name + ", FieldType: " + field.FieldType.ToString());
            }
        }
        /// <summary>
        /// show all the properties of the object
        /// </summary>
        /// <typeparam name="T">object type: class</typeparam>
        /// <param name="obj"></param>
        public static void __DebugAllProperties<T>(T obj) where T : class
        {
            if (obj == null) { Debug.LogError("Null"); return; }
            if (obj.GetType().GetProperties() == null) { Debug.LogError("Null"); return; }

            foreach (var property in obj.GetType().GetProperties())
            {
                Debug.Log("property.Name: " + property.Name + ", propertyType: " + property.PropertyType.ToString());
            }
        }
        public static void __DebugPropertyInfo(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) { Debug.LogError("Null"); return; }

            Debug.Log("PROPERTY..................");
            Debug.Log("Name: " + propertyInfo.Name.ToString());
            Debug.Log("PropertyType.GetMethod: " + propertyInfo.GetMethod.ToString());
            Debug.Log("PropertyType.GetMethod.IsPublic: " + propertyInfo.GetMethod.IsPublic.ToString());
            Debug.Log("PropertyType.GetMethod.IsPrivate: " + propertyInfo.GetMethod.IsPrivate.ToString());
            Debug.Log("PropertyType.IsPublic: " + propertyInfo.PropertyType.IsPublic.ToString());
            Debug.Log("PropertyType.IsNotPublic: " + propertyInfo.PropertyType.IsNotPublic.ToString());
            Debug.Log("PropertyType.IsVisible: " + propertyInfo.PropertyType.IsVisible.ToString());
            Debug.Log("PropertyType.IsNestedFamily: " + propertyInfo.PropertyType.IsNestedFamily.ToString());
            Debug.Log("PropertyType.Assembly: " + propertyInfo.PropertyType.Assembly.ToString());
            Debug.Log("PropertyType.FullName: " + propertyInfo.PropertyType.FullName.ToString());
            Debug.Log("IsPublic: " + propertyInfo.GetType().IsPublic.ToString());
            Debug.Log("IsNotPublic: " + propertyInfo.GetType().IsNotPublic.ToString());
            Debug.Log("IsNested: " + propertyInfo.GetType().IsNested.ToString());
            Debug.Log("IsNestedPrivate: " + propertyInfo.GetType().IsNestedPrivate.ToString());
            Debug.Log("IsNestedPublic: " + propertyInfo.GetType().IsNestedPublic.ToString());
            Debug.Log("----------------------------------------------------------------------------------------");

        }
        public static void __DebugFieldInfo(FieldInfo fieldInfo)
        {
            if (fieldInfo == null) { Debug.LogError("Null"); return; }
            Debug.Log("FIELD..................");
            Debug.Log("Name: " + fieldInfo.Name.ToString());
            Debug.Log("IsPrivate: " + fieldInfo.IsPrivate.ToString());
            Debug.Log("IsPublic: " + fieldInfo.IsPublic.ToString());
            Debug.Log("IsFamily: " + fieldInfo.IsFamily.ToString());
            Debug.Log("IsAssembly: " + fieldInfo.IsAssembly.ToString());
            Debug.Log("IsPublic: " + fieldInfo.GetType().IsPublic.ToString());
            Debug.Log("IsNotPublic: " + fieldInfo.GetType().IsNotPublic.ToString());
            Debug.Log("IsNested: " + fieldInfo.GetType().IsNested.ToString());
            Debug.Log("IsNestedPrivate: " + fieldInfo.GetType().IsNestedPrivate.ToString());
            Debug.Log("IsNestedPublic: " + fieldInfo.GetType().IsNestedPublic.ToString());
            Debug.Log("----------------------------------------------------------------------------------------");
        }
        public static void __DebugRequest(UnityWebRequest unityWebRequest)
        {
            if (unityWebRequest == null) { Debug.LogError("Null error"); return; }

            Debug.LogError("REQUEST...................................................");
            Debug.Log("downloadHandler.method: " + unityWebRequest.method);
            if (unityWebRequest.uploadHandler != null)
                if (unityWebRequest.uploadHandler.data != null)
                    Debug.Log("downloadHandler.uploadHandler.data: " + unityWebRequest.uploadHandler.data);
            Debug.Log("downloadHandler.text: " + unityWebRequest.downloadHandler.text);
            Debug.Log("error: " + unityWebRequest.error);
            Debug.Log("url: " + unityWebRequest.url);
            Debug.Log("isNetworkError: " + unityWebRequest.isNetworkError);
            Debug.Log("isHttpError: " + unityWebRequest.isHttpError);
            Debug.Log("isDone: " + unityWebRequest.isDone);
            Debug.Log("timeout: " + unityWebRequest.timeout);
            Debug.Log("responseCode: " + unityWebRequest.responseCode);
            Debug.LogError("REQUEST...................................................");
        }

    }
}