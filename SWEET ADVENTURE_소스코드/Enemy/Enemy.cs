using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //protected PlayerMove player;
    protected Rigidbody2D rigid;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected int chiliMove = 1;
    protected bool damaged = false;
    protected bool isPerception;
    protected bool isAttack;
    protected IEnumerator myCoroutine;


    //public GameObject item;
    public GameObject myHitEffect;
    public float curTime;
    public float coolTime = 5;

    public GameObject HPBarBackground;
    public Image HPBar_5;

    public int Maxhp;
    public int hp;
    public float atkDistance;
    public AudioClip clip;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCoroutine = ChageMoveCoroutine();
        StartCoroutine(myCoroutine);
        anim.SetBool("isWalking", true);
        //player = FindObjectOfType<PlayerMove>();

        Maxhp = hp = 10;
    }



    protected void ChangeMove()
    {
        chiliMove *= -1;

        if (chiliMove == -1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            HPBarBackground.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (chiliMove == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            HPBarBackground.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void EnemyDestroy()
    {
        Destroy(this.gameObject);
    }


    public void OnDamaged(Vector2 targetPos)
    {
        if (!damaged)
        {
            MyHitEffect();
            gameObject.layer = 14;
            hp -= 5;

            
            HPBarBackground.SetActive(true);
            StartCoroutine(WaitHPBarCoroutine());

            CancelInvoke();
            Invoke("AttackEnd", 0.5f);
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            damaged = true;
            rigid.velocity = new Vector2(0, 0);
            int direc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(direc, 1) * 7, ForceMode2D.Impulse);


            anim.SetTrigger("doDamaged");
            if (hp <= 0)
            {
                HPBar_5.gameObject.SetActive(true);
                HPBarBackground.SetActive(false);
                StopAllCoroutines();
                gameObject.layer = 18;
                /*if(item != null) // 사망시 아이템 드롭
                {
                    item.gameObject.SetActive(true);
                }*/
                GameManager.instance.score += 150;
                Invoke("EnemyDestroy", 1);
            }
            else
            {
                Invoke("OffDamaged", 0.6f);
            }
        }

    }

    IEnumerator WaitHPBarCoroutine()
    {
        yield return new WaitForSeconds(3f);
        HPBarBackground.SetActive(false);
    }

    protected IEnumerator ChageMoveCoroutine()
    {
        yield return new WaitForSeconds(3f);
        if (!isPerception&&!isAttack)
        {
            ChangeMove();
        }
        StartCoroutine(ChageMoveCoroutine());
    }


    protected void OffDamaged()
    {
        gameObject.layer = 12;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        damaged = false;
    }

    protected void Follow()
    {
        ShowPlayer();
        anim.SetBool("isWalking", true);

        rigid.velocity = new Vector2((PlayerMove.instance.transform.position - transform.position).normalized.x * 2, rigid.velocity.y);
    }

    protected void ShowPlayer()
    {
        if ((PlayerMove.instance.transform.position - transform.position).normalized.x < 0)
        {
            chiliMove = -1;
            transform.eulerAngles = new Vector3(0, 180, 0);
            HPBarBackground.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if ((PlayerMove.instance.transform.position - transform.position).normalized.x > 0)
        {
            chiliMove = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
            HPBarBackground.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void MyHitEffect()
    {
        Instantiate(myHitEffect, new Vector3(transform.position.x, transform.position.y , -9), transform.rotation);
    }

    protected void Sound()
    {
        SoundManager.instance.SFXPlay("Attack", clip);
    }

    protected void AttackStart()
    {
        isAttack = true;
        StopCoroutine(myCoroutine);
        StartCoroutine(myCoroutine);
    }
    protected void AttackEnd()
    {
        isAttack = false;
    }

}
