using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    PlayerMove player;
    public GameObject playerHitEffect;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Awake()
    {
        
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Invoke("DistroyThis", 3.0f);
        player = FindObjectOfType<PlayerMove>();
    }


    void DistroyThis()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.tag == "Player")
            {
                CancelInvoke();
                PlayerHitEffect();
                player.OnDamaged(this.transform.position);
                Destroy(this.gameObject);
            }
        }
    }

    public void PlayerHitEffect()
    {
        Instantiate(playerHitEffect, new Vector3(player.transform.position.x, player.transform.position.y, -11), player.transform.rotation);
    }
}
