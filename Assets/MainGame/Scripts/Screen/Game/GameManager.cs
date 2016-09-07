using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private List<Hex> listHex;
    [SerializeField]
    private Transform parentHex;
    [SerializeField]
    private Hex lastHex;
    private Hex firstHex;
    [SerializeField]
    private Hex currHex;

    [SerializeField]
    private Ball ball;
    [SerializeField]
    private Hex hexModel;
    private int score;
    [SerializeField]
    private tk2dTextMesh txtScore;
    [SerializeField]
    private tk2dSprite sprEndgame;
    [SerializeField]
    private ParticleSystem parBallExplo;
    [SerializeField]
    private GameObject goIntro;
    public Vector3 posListHex;
    // Use this for initialization
    void Start()
    {
        Config.currState = GAME_STATE.INTRO;
        StartGenMap();
        score = 0;
        txtScore.text = "0";
        posListHex = parentHex.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Config.currState == GAME_STATE.PLAY)
        {
            // Update position list hex
            if (ball.transform.position.y < 0)
            {

            }
            else
            {
                posListHex.y -= ball.transform.position.y;
                parentHex.transform.position = posListHex;
            }
            // Update delete/spawn list hex
            if (firstHex.transform.position.y < -14f)
            {
                listHex.Remove(firstHex);
                firstHex = listHex[0];
            }
            if (lastHex.transform.position.y < 14f)
            {
                AddNewHex();
            }
        }
    }


    private void StartGenMap()
    {
        while (lastHex.transform.position.y < 14f)
        {

            AddNewHex();
        }
        currHex = listHex[1];
        firstHex = listHex[0];



    }

    public void Play()
    {
        Debug.Log("play");
        goIntro.gameObject.SetActive(false);
        Config.currState = GAME_STATE.PLAY;
        currHex.SetSelective();

        ball.SetMoving(currHex.transform.localPosition);
    }

    private void AddNewHex()
    {
        GameObject newHex = Instantiate(hexModel.gameObject) as GameObject;
        newHex.transform.SetParent(parentHex);
        Hex hexScript = newHex.GetComponent<Hex>();

        int optionNewHex = -1;
        POS_HEX newPosHext = POS_HEX.LEFT;
        Vector3 newPos = lastHex.transform.localPosition;
        if (lastHex.typePos == POS_HEX.MIDDLE)
        {
            optionNewHex = Random.Range(0, 3);
        }
        else if (lastHex.typePos == POS_HEX.LEFT)
        {
            optionNewHex = Random.Range(1, 3);
        }
        else
        {
            optionNewHex = Random.Range(0, 2);
        }

        if (optionNewHex == 0)
        {
            newPos.x -= 1.67f;
            newPos.y += 0.98f;
            lastHex.SetOpenTop(0);

            if (lastHex.typePos == POS_HEX.MIDDLE)
            {
                newPosHext = POS_HEX.LEFT;

            }
            else
            {
                newPosHext = POS_HEX.MIDDLE;

            }
            hexScript.SetOpenBot(2, newPosHext);
        }
        else if (optionNewHex == 1)
        {
            newPos.y += 1.968f;
            lastHex.SetOpenTop(1);
            newPosHext = lastHex.typePos;
            hexScript.SetOpenBot(1, newPosHext);

        }
        else
        {
            newPos.x += 1.67f;
            newPos.y += 0.98f;
            lastHex.SetOpenTop(2);

            if (lastHex.typePos == POS_HEX.MIDDLE)
            {
                newPosHext = POS_HEX.RIGHT;
            }
            else
            {
                newPosHext = POS_HEX.MIDDLE;
            }
            hexScript.SetOpenBot(0, newPosHext);
        }
        hexScript.transform.localPosition = newPos;
        lastHex = hexScript;
        listHex.Add(lastHex);
    }

    public void ClickLeft()
    {
        if (Config.currState == GAME_STATE.PLAY)
        {
            currHex.RotateLeft();
        }
    }

    public void ClickRight()
    {
        if (Config.currState == GAME_STATE.PLAY)
        {
            currHex.RotateRight();
        }
    }

    public void GetNextBallDest()
    {

        //  AddScore();

        ball.SetMoving(currHex.transform.localPosition);

    }

    public void CallNextHexFlash()
    {
        SoundManager.Instance.PlayTing();
        AddScore();
        Hex nextCurrHex = null;
        for (int i = 0; i < listHex.Count; i++)
        {
            if (listHex[i] == currHex)
            {
                nextCurrHex = listHex[i + 1];
                break;
            }
        }
        nextCurrHex.SetSelective();
        currHex.SetUnSelective();
        currHex = nextCurrHex;
    }

    public void AddScore()
    {
        score++;
        txtScore.text = "" + score;
    }

    public void GameOver()
    {
        Config.currState = GAME_STATE.END;
        Prefs.Instance.SetLastScore(score);
        Prefs.Instance.AddCountPlay();
        Analitics();
        StartCoroutine(IEGameOver());
    }

    IEnumerator IEGameOver()
    {
        ball.gameObject.SetActive(false);
        SoundManager.Instance.PlayExplo();
        parBallExplo.transform.position = ball.transform.position + new Vector3(-0.26f, -0.22f, 0);
        parBallExplo.Play();
        Color c = new Color(0, 0, 0, 0);
        float p = 0;
        while (p <= 1)
        {
            p += Time.deltaTime;
            c.a = p;
            sprEndgame.color = c;
            yield return null;
        }

        if (Prefs.Instance.GetCountPlay() % 3 == 0)
        {
            ShowAd();
        }

        Application.LoadLevel(Application.loadedLevel);

        yield return null;
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }

    public void Analitics()
    {
        Analytics.CustomEvent("EndGame", new Dictionary<string, object>  {
    { "score", Prefs.Instance.GetLastScore() },
    { "best", Prefs.Instance.GetBestScore() },
    { "countplay", Prefs.Instance.GetCountPlay() }  });
    }
}
