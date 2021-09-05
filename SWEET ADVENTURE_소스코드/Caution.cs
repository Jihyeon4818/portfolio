using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Caution : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActiveFalse", 6);
        transform.DOMoveY(270, 2);
        transform.DOMoveY(810, 2).SetDelay(4);
    }

    // Update is called once per frame
    void ActiveFalse()
    {
        UI.SetActive(false);
    }
}
