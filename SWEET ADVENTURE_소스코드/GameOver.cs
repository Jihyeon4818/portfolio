using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverbtn;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetBool("skip", true);
        }
    }

    void ButtonOn()
    {
        gameOverbtn.SetActive(true);
    }
}
