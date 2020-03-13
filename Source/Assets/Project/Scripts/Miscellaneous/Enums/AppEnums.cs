using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.AppEnums
{
    public enum Tag
    {
        None = 0,
        Tractor = 1,
        Floor = 2,
        DrawLineController = 3,
        MiniMapCameraHandler = 4,
        Player = 5,
        PlayerCamera = 6,
        GpsHandler = 7,
    }
    public enum HandlerTag
    {
        None,
        DrawLineHandler, 
        MiniMapCameraHandler,
        PlayerHandler,
        PlayerCameraHandler,
        GpsHandler,
        RearBarHandler,
        TractorMovementHandler,
        BakedLineManager,
        HudLeftButtonsGroupHandler,
        MainCameraHandler,
        AlignmentBarHandler,
        SpeedometerHandler,
        MiniMapHudHandler,

        BtnDrawLineModeHandler,
        BtnLightModeHandler,
        BtnCameraModeHandler,
        BtnExitHandler,

        GnsPanelHandler,
        GnsHudHandler,
        CalibrationHandler,
        EnvironmentStyleHandler,

        MainPanelHandler,
        MainPanelTractor3DModel,
        SettingsPanelHandler,
        LoadWorkPanelHandler,

        SaveWorkPanelHandler,

    }
    public enum SkyboxStyle
    {
        None,
        SkyDay,
        SkyNight,
    }

    public enum FloorStyle
    {
        None,
        GreenNight,
        GreenDay,
    }

    /// <summary>
    /// Eliminar esta enum
    /// </summary>
    public enum HudEvent
    {
        None,
        ChangeCameraState,
        ChangeLightState,
        ChangeDrawLineState,
        Exit,
        ChangeGpsState,
        ShowIndicator_Disctance_1,
        ShowIndicator_AB_2,
        ShowIndicator_Gns_3,
        ShowIndicator_overlap_4,
    }
}