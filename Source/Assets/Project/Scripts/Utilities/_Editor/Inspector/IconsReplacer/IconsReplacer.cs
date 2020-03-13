#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Cofradinn.Utilities.IconsReplacer
{
    public enum IconType
    {
        None,
        Handler,
        Presenter,
        Controller,
        System,
        Element,
        Example,
        Mediator,
        IndependientComponent,
    }

    public static class IconsReplacer
    {
        private static bool _iconsLoaded = false;
        private static Dictionary<string, Texture2D> _icons;
        public static void __PutMyIcon(Component comp, IconType iconType)
        {
            if (!_iconsLoaded) __FindIcons();
            DrawIcon(comp, 0, iconType);
        }
        private static void DrawIcon(Component comp, int idx, IconType iconType)
        {
            var largeIcons = GetTextures("sv_label_", string.Empty, 0, 8);
            Texture2D texture2D = null;
            _icons.TryGetValue(iconType.ToString(), out texture2D);
            var icon = new GUIContent(texture2D);// largeIcons[idx];
            var egu = typeof(EditorGUIUtility);
            var flags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
            var args = new object[] { comp, icon.image };
            var setIcon = egu.GetMethod("SetIconForObject", flags, null, new Type[] { typeof(UnityEngine.Object), typeof(Texture2D) }, null);
            setIcon.Invoke(null, args);
        }
        private static void __FindIcons()
        {
            _iconsLoaded = true;
            UnityEngine.Object[] icons = Resources.LoadAll("Cofradinn/Icons/", typeof(Sprite));

            _icons = new Dictionary<string, Texture2D>();

            foreach (var item in icons)
            {
                Sprite sp = (Sprite)item;
                _icons.Add(item.name, sp.texture);
            }
        }
        private static GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
        {
            GUIContent[] array = new GUIContent[count];
            for (int i = 0; i < count; i++)
            {

                array[i] = EditorGUIUtility.IconContent(baseName + (startIndex + i) + postFix);
            }
            return array;
        }
    }
}
#endif