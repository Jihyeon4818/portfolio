using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public GameObject buff;
    public GameObject areYR;
    public GameObject stage;
    public GameObject start;
    public float second;
    public bool isAYR;
    public bool isStart;
    public bool done;
    public bool isBuff;
    public bool buffUI;
    // Start is called before the first frame updatep
    void Start()
    {
        done = false;
        isBuff = true;
        //Invoke("Donefalse", second);
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            Color buffColor = buff.GetComponent<Text>().color;
            Color color = areYR.GetComponent<Image>().color;
            Color color2 = start.GetComponent<Image>().color;
            if(buffUI)
            {
                if (buffColor.a < 1 && isBuff)
                {
                    buffColor.a += Time.deltaTime * 0.8f;
                }
                else
                {
                    isBuff = false;
                }
                if (!isBuff)
                {
                    if (buffColor.a > 0)
                    {
                        buffColor.a -= Time.deltaTime * 0.8f;
                    }
                    else
                    {
                        buffUI = false;
                    }
                }

            }
            else
            {
                buffUI = false;
            }
            


            if (!isAYR && !buffUI)
            {
                if (color.a < 1)
                {
                    color.a += Time.deltaTime * 0.8f;
                }
                else
                {
                    isAYR = true;
                }
            }
            else if (!isStart && isAYR)
            {
                if (color.a > 0)
                {
                    color.a -= Time.deltaTime * 0.8f;
                }
                else if (color2.a < 1)
                {
                    color2.a += Time.deltaTime * 0.8f;
                }
                else
                {
                    isStart = true;
                }
            }
            if (isStart)
            {
                if (color2.a > 0)
                {
                    color2.a -= Time.deltaTime * 0.8f;
                    
                }
                else
                {
                    PlayerMove.instance.uiStart = true;
                    GameManager.instance.gameUI.SetActive(true);
                    done = true;
                }

            }
            areYR.GetComponent<Image>().color = color;
            if (stage != null)
            {
                stage.GetComponent<Image>().color = color;
            }
            start.GetComponent<Image>().color = color2;
            buff.GetComponent<Text>().color = buffColor;
        }

    }

    void Donefalse()
    {
        done = false;
    }
}
