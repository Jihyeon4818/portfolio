using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    Animator anim;
    public bool isPoint;
    public GameObject myEffect;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            if(anim.GetBool("isOpen") == false)
            {
                anim.SetBool("isOpen", true);
                PlayerTriggerEffect();
                SoundManager.instance.SFXPlay("Attack", clip);
            }

        }
    }


    public void PlayerTriggerEffect()
    {
        Instantiate(myEffect, new Vector3(transform.position.x, transform.position.y, -9), transform.rotation);
    }
}
