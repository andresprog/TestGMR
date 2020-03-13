using UnityEngine;

namespace Cofradinn.Systems.GameSettings
{
    public static class GameSettingsPlayerPrefsHelper
    {
        public static void __Save(GameSettingsData parameter)
        {
            // serializar (convertir un json a clase) 
            string json = JsonUtility.ToJson(parameter);
            PlayerPrefs.SetString("SettingsData", json);
            PlayerPrefs.Save();
        }
        public static GameSettingsData __Load()
        {
            // deserializar (convertir una clase a json)
            string json = PlayerPrefs.GetString("SettingsData");
            GameSettingsData data = JsonUtility.FromJson<GameSettingsData>(json) as GameSettingsData;
            return data;
        }
    }
}