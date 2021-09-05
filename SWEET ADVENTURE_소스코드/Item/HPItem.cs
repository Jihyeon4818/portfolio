using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    public GameObject myEffect;
    public bool isActive;
    int dir = 1;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("ChangeMove", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Color color = sprite.color;
        if (isActive == true)
        {
            if (color.a > 0)
            {
                color.a -= Time.deltaTime;
            }
        }
        sprite.color = color;
        if(color.a <= 0)
        {
            Destroy(this.gameObject);
        }
        rigid.velocity = new Vector2(rigid.velocity.x, dir*0.4f);
    }

    protected void ChangeMove()
    {
        dir *= -1;
        Invoke("ChangeMove", 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isActive == false)
            {
                isActive = true;
                PlayerTriggerEffect();
                SoundManager.instance.SFXPlay("Attack", clip);
                if (PlayerMove.instance.hp < 6)
                {
                    PlayerMove.instance.hp += 1;
                }
                else
                {
                    GameManager.instance.score += 50;
                }
            }
        }
    }


    public void PlayerTriggerEffect()
    {
        Instantiate(myEffect, new Vector3(transform.position.x, transform.position.y, -9), transform.rotation);
    }
}
