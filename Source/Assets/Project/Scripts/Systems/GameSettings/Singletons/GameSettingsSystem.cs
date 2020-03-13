using Cofradinn.Modules.Utilities;
using Cofradinn.Utilities.Mappers;
using UnityEngine;

namespace Cofradinn.Systems.GameSettings
{
    public class GameSettingsSystem : Singleton<GameSettingsSystem>
    {
        [Header("Settings")]
        public GameSettingsData _data;

        public void __SaveDataInPlayerPrefs()
        {
            GameSettingsPlayerPrefsHelper.__Save(_data);
        }
        public void __LoadDataInPlayerPrefs()
        {
            _data = GameSettingsPlayerPrefsHelper.__Load();
        }

        private const string DATA_PATH = "PersistenceData/GameSettingsSO";
        protected override void OnAwake()
        {
            base.isPersistent = true;
            GameSettingsScrObj ScriObj = Resources.Load<GameSettingsScrObj>(DATA_PATH) as GameSettingsScrObj;
            _data = new GameSettingsData();
            Mapper.__MapObjects(ScriObj._gameSettings, _data);
        }
    }
}