using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

    public GameManager gameManager;
    public tk2dTextMesh txtBestscore;
    public tk2dTextMesh txtLastscore;
    public tk2dSprite sprSoundOn;
    public tk2dSprite sprSoundOff;
    
    // Use this for initialization
    void Start()
    {
        Config.currState = GAME_STATE.MENU;
        txtBestscore.text = "BEST\n" + Prefs.Instance.GetBestScore();
        txtLastscore.text = "LAST\n" + Prefs.Instance.GetLastScore();
        Config.isSoundOn = Prefs.Instance.isSoundOn();

        if (Config.isSoundOn)
        {
            SoundManager.Instance.PlayMusic();
            sprSoundOn.gameObject.SetActive(true);
            sprSoundOff.gameObject.SetActive(false);
        }
        else
        {
            sprSoundOn.gameObject.SetActive(false);
            sprSoundOff.gameObject.SetActive(true);
        }

    }

    public void ClickStart()
    {
        gameManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClickSoundIc()
    {

        Config.isSoundOn = !Config.isSoundOn;
        Prefs.Instance.SetSound(Config.isSoundOn);
        if (Config.isSoundOn)
        {
            SoundManager.Instance.PlayMusic();
            sprSoundOn.gameObject.SetActive(true);
            sprSoundOff.gameObject.SetActive(false);
        }
        else
        {
            SoundManager.Instance.StopMusic();
            sprSoundOn.gameObject.SetActive(false);
            sprSoundOff.gameObject.SetActive(true);
        }



    }
}
