using UnityEngine;
using System.Collections;

public class Prefs
{
    private static Prefs _instance;

    public static Prefs Instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = new Prefs();

            }
            return _instance;
        }
    }

    public static string KEY_SOUND = "key_sound";
    public static string KEY_BESTSCORE = "key_best_score";
    public static string KEY_LASTSCORE = "key_last_score";
    public static string KEY_COUNT_PLAY = "key_count_play";
    public Prefs()
    {


    }

    public void SetSound(bool on)
    {
        if (on)
        {
            PlayerPrefs.SetInt(KEY_SOUND, 1);
        }
        else
        {
            PlayerPrefs.SetInt(KEY_SOUND, 0);
        }

    }
	public bool isSoundOn()
    {
        return PlayerPrefs.GetInt(KEY_SOUND, 1) == 1;
    }

    public int GetCountPlay()
    {
        return PlayerPrefs.GetInt(KEY_COUNT_PLAY, 0);
    }

    public void AddCountPlay()
    {
        PlayerPrefs.SetInt(KEY_COUNT_PLAY, GetCountPlay() + 1);
    }

    

    public void SetLastScore(int lastScore)
    {
        if (lastScore > PlayerPrefs.GetInt(KEY_BESTSCORE
            ))
        {
            PlayerPrefs.SetInt(KEY_BESTSCORE, lastScore);
        }
        PlayerPrefs.SetInt(KEY_LASTSCORE, lastScore);
    }


    public int GetLastScore()
    {
        return PlayerPrefs.GetInt(KEY_LASTSCORE, 0);
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt(KEY_BESTSCORE, 0);
    }
}
