using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossLine1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(-2167, -506, 0);
        transform.DOLocalMoveX(0, 2);
        transform.DOLocalMoveX(2167, 2).SetDelay(4);
    }

}