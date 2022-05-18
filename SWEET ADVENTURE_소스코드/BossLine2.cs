using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossLine2 : MonoBehaviour
{

    void Start()
    {
        transform.localPosition = new Vector3(2167, 506, 0);
        transform.DOLocalMoveX(0, 2);
        transform.DOLocalMoveX(-2167f, 2).SetDelay(4);

    }


}
