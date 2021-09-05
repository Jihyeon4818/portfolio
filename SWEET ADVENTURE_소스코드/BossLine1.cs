using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossLine1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(480f, 2);
        transform.DOMoveX(1490f, 2).SetDelay(4);
    }

}