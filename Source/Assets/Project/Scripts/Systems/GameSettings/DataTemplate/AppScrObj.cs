using Cofradinn.Data;
using Cofradinn.Systems.GameSettings;
using UnityEngine;

namespace Cofradinn.Systems.GameSettings
{
    [CreateAssetMenu(fileName = "GameSettingsDataSO", menuName = "Cofradinn/GameSettingsData", order = 1)]
    public class GameSettingsScrObj : ScriptableObject
    {
        public GameSettingsData _gameSettings;
    }
}