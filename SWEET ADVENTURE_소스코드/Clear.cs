using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Clear : MonoBehaviour
{

    void Start()
    {
        transform.DOLocalMoveX(520f, 1.5f);
        //transform.DOMoveX(520f, 1.5f);
        transform.DOLocalMoveX(1440f, 0.5f).SetDelay(1.5f);

    }


}