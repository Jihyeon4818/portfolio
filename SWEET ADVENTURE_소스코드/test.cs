using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(205.5f, 1.5f);
        transform.DOMoveX(-997.5f, 0.5f).SetDelay(1.5f);
    }
}
