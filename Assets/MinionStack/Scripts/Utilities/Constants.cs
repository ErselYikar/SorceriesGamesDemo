public class Constants
{
    public const string PLAYER_DATA = "PlayerData";

    public const int TOTAL_PLAYER_COUNT = 20;
    public const int TOTAL_LEVEL_COUNT = 2;

    public const string SOUND_STATE = "SoundState";
    public const string VIBRATION_STATE = "VibrationState";

    public const string SWIPE_TO_PLAY = "SWIPE TO PLAY";
    public const string TAP_TO_PLAY = "TAP TO PLAY";

    public const int LEVEL_COMPLETE_REWARD = 100;
    public const int LEVEL_FAIL_REWARD = 50;
}

public class TAGS
{
    public const string Player = "Player";
    public const string Ground = "Ground";
}

public class AnimationTriggers
{

}

public class DefaultConfigs
{
    //In case of a problem retrieving Remote Config values, default values should be used.

    public const int DEFAULT_FOO = 1; //To be deleted.
}

public class RemoteConfigKeys
{
    //Remote Config Key for Elephant. It should be the same with the key in the Elephant Dashboard.

    public const string REMOTE_FOO = "remote_foo"; //To be deleted.
}

public enum GameMechanics
{
    Swipe = 0,
    Tap = 1,
    TapOnTime = 2,
    Other = 3
}

public enum GuideTypes
{
    Swipe = 0,
    Infinity = 1,
    None = 2
}

public enum Panels
{
    Loading = 0,
    MainMenu = 1,
    Hud = 2,
    Settings = 3,
    Finish = 4,
    MessageBroadccast = 5,
    Tutorial = 6,

    MAX = 7
}

public enum PlayerStates
{
    Selected = 1,
    Open = 2,
    Purchase = 3,

    MAX = 4
}

public enum TapOnTimeTypes
{
    Type3D = 0,
    Type2D = 1
}

public enum TapOnTime2DAlignments
{
    Vertical = 0,
    Horizontal = 1
}

public enum MessageAnimation
{
    Pop = 0,
    Shrink = 1,
    
    MAX = 2
}


#region Localization

public enum CaseType
{
    None = 0,
    AllLower = 1,
    AllUpper = 2,
    TitleCase = 3
}

public enum TextType
{
    Text = 0,
    Tooltip = 1
}

#endregion

#region Sounds

public enum ClickSounds
{
    Click = 0,
    Purchase = 1,
    Upgrade = 2,

    MAX = 3
}

public enum GameStateSounds
{
    Countdown = 0,
    ReadyGo = 1,
    LevelSuccess = 2,
    LevelFinish = 3,

    MAX = 4
}

public enum PlayerInteractionSounds
{
    Fail = 0,
    OnFire = 1,
    LevelWin = 2,

    MAX = 3
}

#endregion

#region Confetti Types

public enum ConfettiTypes
{
    Directional = 0,
    Explosion = 1,
    Fountain = 2
}

public enum ConfettiColors
{
    Green = 0,
    Halloween = 1,
    Magic = 2,
    Purple = 3,
    Rainbow = 4,
    Romantic = 5
}

#endregion