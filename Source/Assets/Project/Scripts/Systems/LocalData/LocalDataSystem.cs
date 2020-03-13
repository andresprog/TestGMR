using UnityEngine;

namespace Cofradinn.Systems.LocalData
{
    public class LocalDataSystem : SingletonComponent<LocalDataSystem>
    {
        public void __Save(string key, string json) 
        {
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
        public string __Load(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        protected override void OnAwake()
        {
            // throw new System.NotImplementedException();
        }
    }
}
