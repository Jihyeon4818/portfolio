using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Clear : MonoBehaviour
{

    void Start()
    {

        transform.DOMoveX(720f, 1.5f);
        transform.DOMoveX(1440f, 0.5f).SetDelay(1.5f);

    }


}