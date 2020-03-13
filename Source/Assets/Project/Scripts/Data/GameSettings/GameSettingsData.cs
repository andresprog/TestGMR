using Cofradinn.AppEnums;
using Cofradinn.Data;
using UnityEngine;

namespace Cofradinn.Systems.GameSettings
{
    [System.Serializable]
    public class GameSettingsData
    {
        // mesh line
        [SerializeField] public float _positionWorkInY;
        [SerializeField] public float _positionWorkInZ;

        // environment
        [SerializeField] public SkyboxStyle _skyboxStyle;
        [SerializeField] public FloorStyle _floorStyle;

        // line properties
        [SerializeField] public MyColor _drawLineColor;
        [SerializeField] public MyColor _parallelLineColor;
        [SerializeField] public float _workWidth;
        [SerializeField] public float _aligmentBarDistancePerLed;
    }
}