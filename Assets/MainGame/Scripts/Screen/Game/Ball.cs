using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
public class Ball : MonoBehaviour
{

    public bool isMoving;
    public GameManager gameManager;
    public float eV;
    private Vector3 tmpCal;

    private Vector3 angle;

    void Start()
    {
        angle = transform.eulerAngles;
        eV = 1;
    }

    void Update()
    {
        angle.z += Config.VEL_BALL_ROTATE;
        transform.eulerAngles = angle;

    }

    public void SetMoving(Vector3 end)
    {
       
        StartCoroutine(IEMoving(end));
    }

    IEnumerator IEMoving(Vector3 end)
    {
        eV += 0.25f;
        Vector3 start = transform.localPosition;

        float timeMove = (start - end).sqrMagnitude / (Config.VEL_BALL_MOVE * eV);
        tmpCal = start;
        float p = 0;
        bool callChangeHexSelect = false;
        while (p <= 1 && Config.currState == GAME_STATE.PLAY)
        {
            p += Time.deltaTime / timeMove;
            if (!callChangeHexSelect && p >= 0.5f)
            {
                callChangeHexSelect = true;
                gameManager.CallNextHexFlash();
            }
            tmpCal.x = start.x * (1 - p) + end.x * p;
            tmpCal.y = start.y * (1 - p) + end.y * p;
            transform.localPosition = tmpCal;
            yield return null;

        }

        isMoving = false;
        if (Config.currState == GAME_STATE.PLAY)
        {
            gameManager.GetNextBallDest();
        }
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Config.currState == GAME_STATE.PLAY)
        {
            gameManager.GameOver();
        }
    }
}
