using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Hex : MonoBehaviour
{

    public POS_HEX typePos;
    [SerializeField]
    private tk2dSprite coreHext;

    private bool isSelective;
    [SerializeField]
    private GameObject linkBot1, linkBot2, linkBot3;
  
    [SerializeField]
    private GameObject bot1, bot2, bot3, top1, top2, top3;

    [SerializeField]
    private Transform coreRotHex;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // pos : 0..2
    public void SetOpenBot(int pos, POS_HEX typePos)
    {
        this.typePos = typePos;
        if (pos == 0)
        {

            bot1.gameObject.SetActive(false);
        }
        else if (pos == 1)
        {

            bot2.gameObject.SetActive(false);
        }
        else if (pos == 2)
        {

            bot3.gameObject.SetActive(false);
        }
        Vector3 angle = coreRotHex.eulerAngles;
        angle.z += Random.Range(0,6)*60f;
        coreRotHex.eulerAngles = angle;
    }
    // pos : 0..2
    public void SetOpenTop(int pos)
    {
        if (pos == 0)
        {
            //linkBot3.gameObject.SetActive(true);
            top1.gameObject.SetActive(false);
        }
        else if (pos == 1)
        {
            //linkBot2.gameObject.SetActive(true);
            top2.gameObject.SetActive(false);
        }
        else if (pos == 2)
        {
            //linkBot1.gameObject.SetActive(true);
            top3.gameObject.SetActive(false);
        }
    }


    public void RotateRight()
    {
        Vector3 angle = coreRotHex.eulerAngles;
        angle.z += 60f;
        coreRotHex.eulerAngles = angle;
    }

    public void RotateLeft()
    {
        Vector3 angle = coreRotHex.eulerAngles;
        angle.z -= 60f;
        coreRotHex.eulerAngles = angle;
    }

    public void SetSelective()
    {
        StartCoroutine(IESelective());
    }

    public void SetUnSelective()
    {
        isSelective = false;
    }

    IEnumerator IESelective()
    {
        bool isSelectColor = false;
        isSelective = true;
        while (isSelective)
        {
            if (isSelectColor)
            {
                coreHext.color = Config.COLOR_HEX_SELECTIVE;
            }
            else
            {
                coreHext.color = Config.COLOR_HEX_UNSELECTIVE;
            }
            isSelectColor = !isSelectColor;
            yield return new WaitForSeconds(0.1f);
        }
        coreHext.color = Config.COLOR_HEX_UNSELECTIVE;
    }
}
