using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (Config.currState == GAME_STATE.MENU)
            {
                Application.Quit();
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}

