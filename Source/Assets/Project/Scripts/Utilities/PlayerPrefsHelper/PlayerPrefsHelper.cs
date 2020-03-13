using UnityEngine;

namespace Cofradinn.Utilities.PlayerPref
{
    public static partial class LocalDataKey 
    {
        public const string NONE = "None";
    }

    public static class PlayerPrefsHelper
    {
        public static void __SaveData(string key, string json)
        {
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
        public static string __LoadData(string key)
        {
            return PlayerPrefs.GetString(key);
        }
    }
}
