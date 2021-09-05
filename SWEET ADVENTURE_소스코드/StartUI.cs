using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public GameObject gradation;
    public GameObject areYR;
    public GameObject stage;
    public GameObject start;
    public float second;
    public bool isAYR;
    public bool isStart;
    public bool done;
    // Start is called before the first frame updatep
    void Start()
    {
        done = true;
        Invoke("Donefalse", second);
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            Color color = areYR.GetComponent<SpriteRenderer>().color;
            Color color2 = start.GetComponent<SpriteRenderer>().color;
            if (!isAYR)
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
            else if (!isStart)
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
                    gradation.GetComponent<SpriteRenderer>().color = color2;
                }
                else
                {
                    PlayerMove.instance.uiStart = true;
                    GameManager.instance.gameUI.SetActive(true);
                    done = true;
                }

            }
            areYR.GetComponent<SpriteRenderer>().color = color;
            stage.GetComponent<SpriteRenderer>().color = color;
            start.GetComponent<SpriteRenderer>().color = color2;
        }

    }

    void Donefalse()
    {
        done = false;
    }
}
