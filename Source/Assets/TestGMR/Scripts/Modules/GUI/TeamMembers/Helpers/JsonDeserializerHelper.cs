/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: TeamMembers
 * ScriptType: Helper
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: viernes, 7 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Collections;
using CetticUtilities.Maths;

namespace Cofradinn.Modules.Gui.TeamMembers
{
    public static class JsonDeserializerHelper
    {
        public static T __FromJson<T>(string json) where T : class
        {
            T obj = Activator.CreateInstance<T>();

            Hashtable table = (Hashtable)easy.JSON.JsonDecode(json);
            Debug.Log("json: " + json);

            foreach (var item in obj.GetType().GetFields())
            {
                //Debug.Log("item.Name: " + item.Name);
                foreach (DictionaryEntry pair in table)
                {
                    //Debug.Log("pair.Key: " + pair.Key);
                    //Debug.Log("pair.value: " + pair.Value);
                    if (pair.Key.ToString() == item.Name)
                    {
                        __SetValue<T>(ref obj, item, pair.Value);
                    }
                }
            }
            return obj;
        }

        private static void __SetValue<M>(ref M modelT, FieldInfo field, object val) where M : class
        {
            //  Debug.Log("property.PropertyType: " + property.PropertyType.ToString());
            if (field.FieldType == typeof(Int32) || field.FieldType == typeof(Int16))
            {
                int n = Maths._Convert_StringToInt(val.ToString());
                field.SetValue(modelT, n);
            }
            else if (field.FieldType == typeof(string) || field.FieldType == typeof(System.String))
            {
                field.SetValue(modelT, val);
            }
            else if (field.FieldType == typeof(bool))
            {
                bool b = Maths._Convert_StringToBool(val.ToString());
                field.SetValue(modelT, b);
            }
            else if (field.FieldType == typeof(DateTime))
            {
                bool b = Maths._Convert_StringToBool(val.ToString());
                field.SetValue(modelT, b);
            }
            else if (field.FieldType == typeof(List<string>) || field.FieldType == typeof(List<System.String>))
            {
                object instance = Activator.CreateInstance(field.FieldType);
                IList list = (IList)instance;

                foreach (var item in (val as ArrayList).ToArray(typeof(string)))
                {
                    //Debug.Log("item: " + item);
                    list.Add(item);
                }
                field.SetValue(modelT, list);
            }
            else if (field.FieldType == typeof(List<Dictionary<string, string>>))
            {
                //Debug.Log("mod: " + modelT);
                //Debug.Log("fname: " + field.Name);
                //Debug.Log("ftype: " + field.FieldType.Name);
                //Debug.Log("val: " + val);

                object instance = Activator.CreateInstance(typeof(List<Dictionary<string, string>>));
                IList list = (IList)instance;

                foreach (var item in (val as ArrayList).ToArray(typeof(object)))
                {
                    Hashtable hash = (Hashtable)(item);
                    Dictionary<string, string> dic = new Dictionary<string, string>();

                    foreach (DictionaryEntry pair in hash)
                    {
                        dic.Add(pair.Key.ToString(), pair.Value.ToString());
                        //Debug.Log("pair.key: " + pair.Key);
                        //Debug.Log("pair.value: " + pair.Value);
                    }
                    list.Add(dic);
                }

                field.SetValue(modelT, list);
            }
            else
            {
                Debug.LogError("Null Type Error: " + field.FieldType.ToString());
            }
        }
    }
}
