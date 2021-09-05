using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossLine2 : MonoBehaviour
{

    void Start()
    {

        transform.DOMoveX(480f, 2);
        transform.DOMoveX(-530f, 2).SetDelay(4);

    }


}
