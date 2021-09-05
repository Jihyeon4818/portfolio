using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Malatang : MonoBehaviour
{
    public MyPlatform platform;
    public Transform pos;
    public Transform pos2;
    public Vector2 boxSize;
    public Vector2 boxSize2;
    public GameObject myEffect;
    public GameObject pattern2Effect;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioClip clip7;
    PlayerMove player;
    Vector3 playerPosSave;
    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D capCollider;
    SpriteRenderer spriteRenderer;
    //bool damaged = false;
    public float jumpPower;
    public int hp = 200;
    public bool isAttack;
    public bool isInvincible;
    float patternAttack = 0;
    float patternAttack2 = 0;
    public float curTime = 5;
    public float coolTime = 5;
    public float InvincibleTime = 8;
    public GameObject playerHitEffect_Dash;
    public GameObject playerHitEffect_Attack;
    public bool ready;

    public bool isDead;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        platform = FindObjectOfType<MyPlatform>();
        Invoke("Ready", 6f);
        ShowPlayer();
        SoundManager.instance.SFXPlay("Ready", clip2);
    }
    void Update()
    {
        if (!isAttack&& ready)
        {
            if (!anim.GetBool("3pattern"))
            {
                if (curTime <= 0)
                {
                    anim.SetTrigger("atk");
                    curTime = coolTime;
                }
            }
            ShowPlayer();
        }


        curTime -= Time.deltaTime;
        InvincibleTime -= Time.deltaTime;

        if (patternAttack>=2)
        {
            anim.SetBool("3Attack3", true);
            patternAttack = 0;
        }


        if(patternAttack2>=2)
        {
            anim.SetBool("3Attack4", true);
            anim.SetBool("3Attack3", false);
        }


        if (isInvincible)
        {
            ShowPlayer();
            if (rigid.velocity.y <= 0.2f)
            {
                rigid.velocity = new Vector2((player.transform.position - transform.position).normalized.x * 2, (player.transform.position - transform.position).normalized.y * 2);
            }
            else
            {
                rigid.velocity = new Vector2((player.transform.position - transform.position).normalized.x * 2, rigid.velocity.y);
            }
        }

    }

    

    public void IsInvincible()
    {
        if (InvincibleTime <= 0)
        {
            anim.SetTrigger("Invincible");
            rigid.gravityScale = 3;
            isInvincible = false;
        }
    }




    void Clear()
    {
        
        GameManager.instance.ReStart();
        SceneManager.LoadScene("3Epilogue");
    }
    void Dead()
    {
        isDead = true;
    }


    public void OnDamaged()
    {
        gameObject.layer = 16;
        hp -= 5;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        if (hp <= 0)
        {
            
            Invoke("Clear", 6);
            Invoke("Dead", 1);
            anim.SetBool("dath", true);
            Invoke("OffDamaged", 0.2f);
            hp = 0;

        }
        else
        {
            Invoke("OffDamaged", 0.2f);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
        Gizmos.DrawWireCube(pos2.position, boxSize2);
    }
    void JumpAttackSound()
    {
        SoundManager.instance.SFXPlay("Attack", clip);
    }

    void ReadyAttack2Sound()
    {
        SoundManager.instance.SFXPlay("Attack3", clip3);
        Invoke("Attack2Sound", 4.0f);
    }

    void Attack2Sound()
    {
        SoundManager.instance.SFXPlay("Attack4", clip4);
    }

    void Attck3Sound()
    {
        SoundManager.instance.SFXPlay("Attack5", clip5);  
    }
    void JumpAttack3Sound()
    {
        SoundManager.instance.SFXPlay("Attack6", clip6);
    }

    void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 9)
            {
                collider.GetComponent<PlayerMove>().OnDamaged(transform.position);
                PlayerHitEffect_Attack();
                PlayerMove.instance.PlaySound("OnDamaged");
            }
        }
    }
    void DashAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 9)
            {
                collider.GetComponent<PlayerMove>().OnDamaged(transform.position);
                PlayerHitEffect_Dash();
                PlayerMove.instance.PlaySound("OnDamaged");
            }
        }
    }

    void OffDamaged()
    {
        gameObject.layer = 15;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void ShowPlayer()
    {
        if ((player.transform.position - transform.position).normalized.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if ((player.transform.position - transform.position).normalized.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    void DashPre() // 대쉬 공격 준비
    {
        isAttack = true;
        rigid.gravityScale = 0;
        //playerPosSave = player.transform.position;
        playerPosSave = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
    void Dash() //대쉬 공격
    {
        transform.position = playerPosSave;
    }

    void JumpAttack1()
    {

        ShowJumpEffect();
        isAttack = true;
        rigid.AddForce(Vector2.up * jumpPower*10, ForceMode2D.Impulse);
        SoundManager.instance.SFXPlay("Jump", clip7);
        rigid.velocity = new Vector2((player.transform.position - transform.position).normalized.x * 20, rigid.velocity.y);
        patternAttack += 1;
    }
    void JumpAttack2()
    {
        isAttack = true;
        rigid.AddForce(Vector2.up * jumpPower * 10, ForceMode2D.Impulse);
        patternAttack2 += 1;
    }
    void JumpAttack3()
    {
        ShowJumpEffect();
        isAttack = true;
        rigid.AddForce(Vector2.up * jumpPower * 2, ForceMode2D.Impulse);
        rigid.gravityScale = 0;
        isInvincible = true;
        InvincibleTime = 8;
        //rigid.velocity = new Vector2((player.position - transform.position).normalized.x, rigid.velocity.y);
    }

    void AttackOver()
    {
        isAttack = false;
    }

    void JoinPattern2()
    {
        capCollider.offset = new Vector2(-0.2f, 1);
        capCollider.size = new Vector2(3.5f,3.5f);
    }
    void JoinPattern3_1()
    {
        capCollider.offset = new Vector2(-0.28f, 0.46f);
        capCollider.size = new Vector2(3.6f, 3.6f);
    }
    void JoinPattern3_2()
    {
        capCollider.offset = new Vector2(-0.28f, 0.46f);
        capCollider.size = new Vector2(3.7f, 3.7f);
    }
    void JoinPattern3_3()
    {
        capCollider.offset = new Vector2(-0.28f, 0.46f);
        capCollider.size = new Vector2(3.8f, 3.8f);
    }
    void JoinPattern3_4()
    {
        capCollider.offset = new Vector2(-0.28f, 0.46f);
        capCollider.size = new Vector2(3.9f, 3.9f);
    }
    void JoinPattern3_5()
    {
        capCollider.offset = new Vector2(-0.28f, 0.46f);
        capCollider.size = new Vector2(4f, 4f);
    }
    void Invincible() //무적
    {
        CancelInvoke();
        gameObject.layer = 16;
    }

    void PlatformOnDamage()
    {
        platform.tag = "PlatformEnemy";
        Invoke("PlatformoffDamage", 1.0f);
    }
    void PlatformoffDamage()
    {
        platform.tag = "Platform";
    }

    void CheckHP()
    {
        if (hp <= 100)
        {
            anim.SetBool("3pattern", true);
        }
        else if (hp <= 150)
        {
            anim.SetBool("2pattern", true);
        }
    }

    void ChangePattern()
    {
        anim.SetBool("3pattern", false);
        anim.SetBool("2pattern", false);
        anim.SetBool("3Attack4", false);
        anim.SetBool("3Attack3", false);
        patternAttack = 0;
        patternAttack2 = 0;
    }

    void ShowJumpEffect()
    {
       Instantiate(myEffect, new Vector3(transform.position.x,transform.position.y-1.5f,transform.position.z), transform.rotation);
    }

    void Show2Effect()
    {
        Instantiate(pattern2Effect, new Vector3(transform.position.x, transform.position.y +1, transform.position.z-1), transform.rotation);
    }

    public void PlayerHitEffect_Attack()
    {
        Instantiate(playerHitEffect_Attack, new Vector3(player.transform.position.x, player.transform.position.y, -9), player.transform.rotation);
    }
    public void PlayerHitEffect_Dash()
    {
        Instantiate(playerHitEffect_Dash, new Vector3(player.transform.position.x, player.transform.position.y, -9), player.transform.rotation);
    }
    void Ready()
    {
        PlayerMove.instance.uiStart = true;
        GameManager.instance.gameUI.SetActive(true);
        Invoke("AttackReady", 2f);
    }
    void AttackReady()
    {
        ready = true;
    }
}
