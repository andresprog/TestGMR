/**************************************************************************
 * Copyright: Copyright 2019 Cofradinn, LLC. All Rights reserved.
 * Module: SettingsPanel
 * ScriptType: Helper
 * Created by: Andrés Romero,andresraulrg@gmail.com
 * Created on: 27/12/19
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using Cofradinn.Utilities.ScriptTemplates;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace Cofradinn.Editor.Utilities
{
    public class EditorScriptTemplates
    {
        private const string _BASE_ASSETS_PATH = "/Project/Scripts/Utilities/_Editor/ScriptTemplate/Templates";
        private const string _BASE_MENU_PATH = "Assets/Create/Cofradinn/Templates";

        [MenuItem(_BASE_MENU_PATH + "/Module + Tag", false, priority: 0)]
        private static void CreateModuleAndCreateTag()
        {
            UnityEngine.Object selected = Selection.activeObject;

            if (selected == null || selected.name.Length == 0)
            {
                Debug.Log("Creation Tag Error: Selected object is not Valid");
                return;
            }

            string tagName = selected.name + "Handler";
            tagName = tagName.Replace("-", "_");
            tagName = tagName.Trim();

            __CreateTag(tagName);
            CreateModule();
        }
        [MenuItem(_BASE_MENU_PATH + "/Module", false, priority: 1)]
        private static void CreateModule()
        {
            string folderName;
            string findPath;
            string template;
            string targetPath;

            UnityEngine.Object selected = Selection.activeObject;
            string currentPathSelected = AssetDatabase.GetAssetPath(Selection.activeObject);

            folderName = "Enums";
            __CreateModuleFolders(currentPathSelected, folderName);
            findPath = _BASE_ASSETS_PATH + "/Enum.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected + "/" + folderName;
            __CreateAsset(selected, template, selected.name + "Enums", targetPath);

            folderName = "Constants";
            __CreateModuleFolders(currentPathSelected, folderName);
            findPath = _BASE_ASSETS_PATH + "/HandlerTag.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected + "/" + folderName;
            __CreateAsset(selected, template, "HandlerTag", targetPath);

            folderName = "Presenters";
            __CreateModuleFolders(currentPathSelected, folderName);
            findPath = _BASE_ASSETS_PATH + "/Presenter.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected + "/" + folderName;
            __CreateAsset(selected, template, selected.name + "Presenter", targetPath);

            folderName = "Handlers";
            __CreateModuleFolders(currentPathSelected, folderName);
            findPath = _BASE_ASSETS_PATH + "/Handler.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected + "/" + folderName;
            __CreateAsset(selected, template, selected.name + "Handler", targetPath);

            folderName = "Examples";
            __CreateModuleFolders(currentPathSelected, folderName);
            findPath = _BASE_ASSETS_PATH + "/Example.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected + "/" + folderName;
            __CreateAsset(selected, template, selected.name + "Example", targetPath);

            Task.Run(__CompileScripts);

            //__CreateModuleFolders(currentPathSelected, "Prefabs");
        }
        [MenuItem(_BASE_MENU_PATH + "/Constants", false, priority: 2)]
        private static void CreateConstants()
        {
            string findPath;
            string template;
            string targetPath;

            UnityEngine.Object selected = Selection.activeObject;
            string currentPathSelected = AssetDatabase.GetAssetPath(Selection.activeObject);

            findPath = _BASE_ASSETS_PATH + "/HandlerTag.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected;
            __CreateAsset(selected, template, "HandlerTag", targetPath);

            Task.Run(__CompileScripts);
            //__CreateModuleFolders(currentPathSelected, "Prefabs");
        }
        [MenuItem(_BASE_MENU_PATH + "/Enums", false, priority: 3)]
        private static void CreateEnums()
        {
            string findPath;
            string template;
            string targetPath;

            UnityEngine.Object selected = Selection.activeObject;
            string currentPathSelected = AssetDatabase.GetAssetPath(Selection.activeObject);

            findPath = _BASE_ASSETS_PATH + "/Enum.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected;
            __CreateAsset(selected, template, selected.name + "Enums", targetPath);

            Task.Run(__CompileScripts);
            //__CreateModuleFolders(currentPathSelected, "Prefabs");
        }
        [MenuItem(_BASE_MENU_PATH + "/Example", false, priority: 4)]
        private static void CreateExample()
        {
            string findPath;
            string template;
            string targetPath;

            UnityEngine.Object selected = Selection.activeObject;
            string currentPathSelected = AssetDatabase.GetAssetPath(Selection.activeObject);

            findPath = _BASE_ASSETS_PATH + "/Example.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected;
            __CreateAsset(selected, template, selected.name + "Example", targetPath);

            Task.Run(__CompileScripts);
            //__CreateModuleFolders(currentPathSelected, "Prefabs");
        }
        [MenuItem(_BASE_MENU_PATH + "/Handler", false, priority: 5)]
        private static void CreateHandler()
        {
            string findPath;
            string template;
            string targetPath;

            UnityEngine.Object selected = Selection.activeObject;
            string currentPathSelected = AssetDatabase.GetAssetPath(Selection.activeObject);

            findPath = _BASE_ASSETS_PATH + "/Handler.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected;
            __CreateAsset(selected, template, selected.name + "Handler", targetPath);

            Task.Run(__CompileScripts);
            //__CreateModuleFolders(currentPathSelected, "Prefabs");
        }
        [MenuItem(_BASE_MENU_PATH + "/Presenter", false, priority: 6)]
        private static void CreatePresenter()
        {
            string findPath;
            string template;
            string targetPath;

            UnityEngine.Object selected = Selection.activeObject;
            string currentPathSelected = AssetDatabase.GetAssetPath(Selection.activeObject);

            findPath = _BASE_ASSETS_PATH + "/Presenter.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected;
            __CreateAsset(selected, template, selected.name + "Presenter", targetPath);

            Task.Run(__CompileScripts);
            //__CreateModuleFolders(currentPathSelected, "Prefabs");
        }
        [MenuItem(_BASE_MENU_PATH + "/Controller", false, priority: 7)]
        private static void CreateController()
        {
            Debug.LogError("Warning: This template is not finished");

            string findPath;
            string template;
            string targetPath;

            UnityEngine.Object selected = Selection.activeObject;
            string currentPathSelected = AssetDatabase.GetAssetPath(Selection.activeObject);

            findPath = _BASE_ASSETS_PATH + "/Controller.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected;
            __CreateAsset(selected, template, selected.name + "Controller", targetPath);

            Task.Run(__CompileScripts);
            //__CreateModuleFolders(currentPathSelected, "Prefabs");
        }
        [MenuItem(_BASE_MENU_PATH + "/System", false, priority: 8)]
        private static void CreateSystem()
        {
            Debug.LogError("Warning: This template is not finished");

            string findPath;
            string template;
            string targetPath;

            UnityEngine.Object selected = Selection.activeObject;
            string currentPathSelected = AssetDatabase.GetAssetPath(Selection.activeObject);

            findPath = _BASE_ASSETS_PATH + "/System.cs.txt";
            template = System.IO.File.ReadAllText(Application.dataPath + findPath);
            template = KeywordsReplacer.__Replace(template, selected.name);
            targetPath = currentPathSelected;
            __CreateAsset(selected, template, selected.name + "System", targetPath);

            Task.Run(__CompileScripts);
            //__CreateModuleFolders(currentPathSelected, "Prefabs");
        }



        private static void __CreateTag(string tagName)
        {
            //https://answers.unity.com/questions/33597/is-it-possible-to-create-a-tag-programmatically.html?_ga=2.107975298.1494784879.1577387337-1661697512.1549593987

            // Open tag manager
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty tagsProp = tagManager.FindProperty("tags");

            //// For Unity 5 we need this too
            //SerializedProperty layersProp = tagManager.FindProperty("layers");

            // First check if it is not already present
            bool found = false;
            for (int i = 0; i < tagsProp.arraySize; i++)
            {
                SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
                if (t.stringValue.Equals(tagName)) { found = true; break; }
            }

            // if not found, add it
            if (!found)
            {
                int newId = tagsProp.arraySize;
                tagsProp.InsertArrayElementAtIndex(newId);
                SerializedProperty n = tagsProp.GetArrayElementAtIndex(newId);
                n.stringValue = tagName;
            }

            // Setting a Layer (Let's set Layer 10)
            // string layerName = "the_name_want_to_give_it";

            //// --- Unity 4 ---
            //SerializedProperty sp = tagManager.FindProperty("User Layer 10");
            //if (sp != null) sp.stringValue = layerName;

            //// --- Unity 5 ---
            //SerializedProperty sp = layersProp.GetArrayElementAtIndex(10);
            //if (sp != null) sp.stringValue = layerName;

            // and to save the changes
            tagManager.ApplyModifiedProperties();
        }
        private static void __CreateModuleFolders(string path, string folderName)
        {
            string guid = AssetDatabase.CreateFolder(path, folderName);
            AssetDatabase.GUIDToAssetPath(guid);
        }
        private static void __CreateAsset(UnityEngine.Object selected, string template, string fileName, string targetPath)
        {
            if (selected == null || selected.name.Length == 0)
            {
                Debug.Log("Selected object not Valid");
                return;
            }
            // remove whitespace and minus
            fileName = fileName.Replace("-", "_");
            string file = fileName + ".cs";
            string copyPath = targetPath + "/" + file;
            Debug.Log("Creating Classfile: " + copyPath);
            if (File.Exists(copyPath) == false)
            {
                // do not overwrite
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    outfile.WriteLine(template);
                }
            }
            AssetDatabase.Refresh();
        }
        private async static void __CompileScripts()
        {
            // Debug.Log("Start 5000");
            await Task.Delay(3000);
            // Console.WriteLine("Finish");
            AssetDatabase.Refresh();
        }
    }
}
#endif