using Cofradinn.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public static class Converter
    {
        /// <summary>
        /// Convert string to enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Enum value in string format</param>
        /// <returns>Enum value</returns>
        public static T __ConvertStringToEnum<T>(string value) where T : struct, IConvertible
        {
            Debug.LogError("Warning: Check this line because is a test");
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static int __ConvertStringToInt(string textNumber)
        {
            return Convert.ToInt32(textNumber);
        }
        public static int? __ConvertStringToNullableInt(string textNumber)
        {
            return Convert.ToInt32(textNumber);
        }
        public static float __ConvertStringToFloat(string textNumber)
        {
            return float.Parse(textNumber);
        }
        public static DateTime __ConvertStringToDateTime(string textDateTime)
        {
            return Convert.ToDateTime(textDateTime);
        }
        public static bool __ConvertStringToBool(string textBool)
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
        /// <summary>
        /// Return #RRGGBB
        /// </summary>
        public static String __HexConverter(Color color)
        {
            return "#" + __ToHex(color.r) + __ToHex(color.g) + __ToHex(color.b);
        }
        /// <summary>
        /// Return RRGGBB
        /// </summary>
        public static String __RGBHexConverter(Color color)
        {
            return __ToHexFromBinary(color.r) + __ToHexFromBinary(color.g) + __ToHexFromBinary(color.b);
        }
        /// <summary>
        /// Return 
        /// </summary>
        public static String __RGBConverter(Color color)
        {
            return "RGB(" + color.r.ToString() + "," + color.g.ToString() + "," + color.b.ToString() + ")";
        }
        /// <summary>
        /// return format hexadecimal "FF"
        /// </summary>
        /// <param name="value">Range [0,255]</param>
        public static string __ToHex(int value)
        {
            return String.Format("{0:X2}", value);
        }
        /// <summary>
        /// return format hexadecimal "FF"
        /// </summary>
        /// <param name="value">Range [0,1]</param>
        public static string __ToHex(float value)
        {
            return String.Format("{0:X2}", (int)(value * 255));
        }
        /// <summary>
        /// return format hexadecimal "FF"
        /// </summary>
        /// <param name="value">Range [0,255]</param>
        public static string __ToHexFromBinary(float value)
        {
            return String.Format("{0:X2}", (int)(value));
        }

        /// <summary>
        /// return Range [0,360]
        /// </summary>
        /// <param name="eulerAngles">Range [-N, +N]</param>
        public static float __ClampAngleBetween_0_and_360(float eulerAngles)
        {
            float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;

            if (result < 0) 
                result += 360f;

            return result;
        }
        /// <summary>
        /// return Range [-180,180]
        /// </summary>
        /// <param name="eulerAngles">Range [-N, +N]</param>
        public static float __ConvertAngle180Format(float eulerAngles)
        {
            float result = __ClampAngleBetween_0_and_360(eulerAngles);

            if (result > 180)
                result = -360f + result;

            return result;
        }

    }
}
