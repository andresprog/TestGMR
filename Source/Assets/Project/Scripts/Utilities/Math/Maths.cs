using Cofradinn.Modules.Gui.TeamMembers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CetticUtilities.Maths
{
    /// <summary>
    /// Librería de matematicas comunes
    /// </summary>
    public static class Maths
    {
        public static string _GetRoute(Transform obj)
        {
            Transform myParent = obj;
            string route = "";
            do
            {
                route = route + "_" + myParent.transform.name;
                myParent = myParent.transform.parent;
            } while (myParent != null);

            return route;
        }
        /// <summary>
        /// Recibe: "epp_no_indicado" ------ Retorna: Constants.Error.epp_no_indicado
        /// </summary>
        public static T _ConvertStringToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        /// <summary>
        /// Retorna un procentaje en valores de [0,1] siendo 1 equivalente a 100%
        /// </summary>
        /// <param name="currentValue">[0,N]</param>
        /// <param name="maxValue">[0,N]</param>
        /// <returns></returns>
        public static float _GetPercent(float currentValue, float maxValue)
        {
            currentValue = Mathf.Abs(currentValue);
            maxValue = Mathf.Abs(maxValue);
            return currentValue / maxValue;
        }
        /// <summary>
        /// Retorna la misma lista desordenada
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="input">lista</param>
        public static List<T> _GetShuffleList<T>(List<T> input)
        {
            if (input != null) return null;
            if (input.Count < 1) return null;

            List<T> arr = input;
            List<T> arrDes = new List<T>();

            UnityEngine.Random randNum = new UnityEngine.Random();
            while (arr.Count > 0)
            {
                int val = UnityEngine.Random.Range(0, arr.Count - 1);
                arrDes.Add(arr[val]);
                arr.RemoveAt(val);
            }

            return arrDes;
        }



        /// <summary>
        /// Get Value of the property by his name, return property value
        /// </summary>
        /// <param name="src">class or object</param>
        /// <param name="propName">Property name</param>
        public static object GetPropertyValue<T>(T src, string propName)
        {
            // Example
            // Customer customer = new Customer() { code = "C001", email = "aaa" };
            // string text = GetPropValue<Customer>(customer, "Code").ToString();
            // Debug.Log("text code: " + text);

            if (src == null) { Debug.LogError("Src Null Error"); return null; }

            Type type = src.GetType();
            PropertyInfo info = type.GetProperty(propName);
            if (info == null) { Debug.LogError("Info Null Error"); return null; }

            object obj = info.GetValue(src);
            if (obj == null) { Debug.LogError("Info Null Error"); return null; }

            return obj;

            //return src.GetType().GetProperty(propName).GetValue(src);
        }
        /// <summary>
        /// Get Value of the property by his name, return property value
        /// </summary>
        /// <param name="src">class or object</param>
        /// <param name="fieldType">Field name</param>
        public static object GetFieldValueByName<T>(T src, string fieldName)
        {
            // Example
            // Customer customer = new Customer() { code = "C001", email = "aaa" };
            // string text = GetPropValue<Customer>(customer, "Code").ToString();
            // Debug.Log("text code: " + text);

            if (src == null) { Debug.LogError("Src Null Error: " + fieldName); return null; }

            Type type = src.GetType();
            FieldInfo info = type.GetField(fieldName);
            if (info == null) { Debug.LogError("Info Null Error: " + fieldName); return null; }

            object obj = info.GetValue(src);
            if (obj == null) { Debug.LogError("Info Null Error: " + fieldName); return null; }

            return obj;

            //return src.GetType().GetProperty(propName).GetValue(src);
        }
        public static object GetFieldValueByRoute<T>(T src, string firstObjName, string fieldName)
        {
            // Esto se puede mejorar ocupando una ruta con / y separandola en un array, luego usar un loop para llegar al field
            if (src == null) { Debug.LogError("Src Null Error: " + firstObjName); return null; }

            object firstObj = GetFieldValueByName(src, firstObjName);
            if (firstObj == null) { Debug.LogError("firstObj Null Error: " + firstObjName); return null; }

            object field = GetFieldValueByName(firstObj, fieldName);
            if (field == null) { Debug.LogError("lastObj Null Error: " + field); return null; }


            Type type = field.GetType();
            FieldInfo info = type.GetField(fieldName);
            if (info == null) { Debug.LogError("Info Null Error: " + field); return null; }

            object obj = info.GetValue(src);
            if (obj == null) { Debug.LogError("Info Null Error: " + field); return null; }

            return obj;

            //return src.GetType().GetProperty(propName).GetValue(src);
        }
        public static object GetFieldValueByRoute<T>(T src, string firstObjName, string secondObjName, string fieldName)
        {
            // Esto se puede mejorar ocupando una ruta con / y separandola en un array, luego usar un loop para llegar al field
            if (src == null) { Debug.LogError("Src Null Error: " + firstObjName); return null; }

            object firstObj = GetFieldValueByName(src, firstObjName);
            if (firstObj == null) { Debug.LogError("firstObj Null Error: " + firstObjName); return null; }

            object secondObj = GetFieldValueByName(firstObj, secondObjName);
            if (secondObj == null) { Debug.LogError("secondObj Null Error: " + secondObjName); return null; }

            object field = GetFieldValueByName(secondObj, fieldName);
            if (field == null) { Debug.LogError("lastObj Null Error: " + field); return null; }


            Type type = field.GetType();
            FieldInfo info = type.GetField(fieldName);
            if (info == null) { Debug.LogError("Info Null Error: " + field); return null; }

            object obj = info.GetValue(src);
            if (obj == null) { Debug.LogError("Info Null Error: " + field); return null; }

            return obj;

            //return src.GetType().GetProperty(propName).GetValue(src);
        }

        //public static object GetFieldValueByType<T>(T src, string fieldType)
        //{
        //    // Example
        //    // Customer customer = new Customer() { code = "C001", email = "aaa" };
        //    // string text = GetPropValue<Customer>(customer, "Code").ToString();
        //    // Debug.Log("text code: " + text);

        //    if (src == null) { Debug.LogError("Src Null Error"); return null; }

        //    Type type = src.GetType();
        //    FieldInfo info = type.GetField(fieldType);
        //    if (info == null) { Debug.LogError("Info Null Error"); return null; }

        //    object obj = info.GetValue(src);
        //    if (obj == null) { Debug.LogError("Info Null Error"); return null; }

        //    return obj;

        //    //return src.GetType().GetProperty(propName).GetValue(src);
        //}

        public static int _Convert_StringToInt(string textNumber)
        {
            return Convert.ToInt32(textNumber);
        }
        public static int? _Convert_StringToNullableInt(string textNumber)
        {
            return Convert.ToInt32(textNumber);
        }
        public static float _Convert_StringToFloat(string textNumber)
        {
            return float.Parse(textNumber);
        }
        public static DateTime _Convert_StringToDateTime(string textDateTime)
        {
            return Convert.ToDateTime(textDateTime);
        }
        public static bool _Convert_StringToBool(string textBool)
        {
            textBool.ToLower(); // all letters in minus
            textBool.Trim(); // Deleta all the empty spaces

            switch (textBool)
            {
                case "true": return true;
                case "false": return false;
                default:
                    Debug.LogError("Bool Null Error");
                    return false;
            }
        }
        public static string[] _Convert_StringToStringArray(string objString)
        {
            int n = objString.GetType().GetFields().Length;
            string[] strArray = new string[n];

            //for (int i = 0; i < n; i++)
            //{
            //    strArray[i] = objString.GetType().GetProperties()[i].GetValue(objString).ToString();
            //    Debug.Log(objString.GetType().GetFields()[i].GetValue(objString).ToString());
            //}
            Hashtable table = (Hashtable)easy.JSON.JsonDecode(objString);

            int count = 0;
            //foreach (DictionaryEntry pair in table)
            //{
            //    //Debug.Log("pair.Key: " + pair.Key);
            //    //Debug.Log("pair.value: " + pair.Value);
            //    strArray[count] = objString.GetType().GetProperties()[count].GetValue(objString).ToString();
            //   // __SetValue<T>(ref obj, item, pair.Value.ToString());
            //    count++;
            //}

            return strArray;
        }
        public static TeamMember[] _Convert_StringToTeamMemberArray(string objString)
        {
            int n = objString.GetType().GetFields().Length;
            TeamMember[] tmArray = new TeamMember[n];

            //for (int i = 0; i < n; i++)
            //    tmArray[i] = objString.GetType().GetFields()[i].GetValue(objString) as TeamMember;

            Hashtable table = (Hashtable)easy.JSON.JsonDecode(objString);

            int count = 0;
            foreach (DictionaryEntry pair in table)
            {
                //Debug.Log("pair.Key: " + pair.Key);
                //Debug.Log("pair.value: " + pair.Value);
                tmArray[count] = objString.GetType().GetFields()[count].GetValue(objString) as TeamMember;
                // __SetValue<T>(ref obj, item, pair.Value.ToString());
                count++;
            }


            return tmArray;
        }

    }
}

