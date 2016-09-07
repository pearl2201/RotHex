using UnityEngine;
using System.Collections;




public class Config
{

    public static float SCREEN_WIDTH = 7.6f;
    public static float SCREEN_HEIGHT = 12.8f;
    public static GAME_STATE currState = GAME_STATE.MENU;
    public static bool isSoundOn = true;
    public static float VEL_BALL_ROTATE = 2.5f;
    public static float VEL_BALL_MOVE = 0.6f;// tile 1m/1s

    public static Vector3 DISTANCE_CAL_HEX_LEFT = new Vector3();
    public static Vector3 DISTANCE_CAL_HEX_RIGHT = new Vector3();
    public static Vector3 DISTANCE_CAL_HEX_MIDDLE  = new Vector3();
    public static Color COLOR_HEX_SELECTIVE = new Color(0, 0.5843f, 0.53f, 1);
    public static Color COLOR_HEX_UNSELECTIVE = new Color(0, 0.4156f, 0.3764f, 1);
}

public enum POS_HEX
{
    MIDDLE, LEFT, RIGHT
}

public enum GAME_STATE
{
    MENU, INTRO, PLAY, END
}