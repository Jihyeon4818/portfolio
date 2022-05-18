using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage : MonoBehaviour
{
    public AudioClip clip;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveX(-520f, 1.5f);
        transform.DOLocalMoveX(-1440f, 0.5f).SetDelay(1.5f);
        Invoke("ClearSound", 1.2f);
        Invoke("SetActiveFalse", 2.5f);
    }

    void ClearSound()
    {
        SoundManager.instance.SFXPlay("Clear", clip);
    }
    void SetActiveFalse()
    {
        parent.SetActive(false);
    }
}
