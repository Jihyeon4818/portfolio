using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    static public PlayerMove instance;

    public bool uiStart;

    public float maxSpeed;
    public float jumpPower;
    public float Speed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public Animator anim;
    bool damaged = false;
    public int hp ;
    public Transform pos;
    public Vector2 boxSize;
    float curTime;
    public float coolTime;
    public AudioClip[] clip;

    public Image HP1;
    public Image HP2;
    public Image HP3;

    bool isAttack = false;

    void Start()
    {
        hp = 6;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        HP1.fillAmount = 1f;
        HP2.fillAmount = 1f;
        HP3.fillAmount = 1f;

    }


    void Update()
    {
        if (uiStart)
        {
            if (!isAttack)
            {
                //점프
                if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
                {
                    PlaySound("Jump");
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    anim.SetBool("isStopping", false);
                    anim.SetBool("isJumping", true);
                }

                //미끄러짐
                if (Input.GetButtonUp("Horizontal"))
                {
                    anim.SetBool("isStopping", true);
                    rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.3f, rigid.velocity.y);
                }

                //방향 전환
                if (Input.GetButton("Horizontal"))
                {
                    anim.SetBool("isStopping", false);
                    //spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
                    if (Input.GetAxisRaw("Horizontal") == -1)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    else if (Input.GetAxisRaw("Horizontal") == 1)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }

                }


                //idle 상태로
                if (Mathf.Abs(rigid.velocity.x) < 0.1 && !anim.GetBool("isJumping"))
                {
                    anim.SetBool("isStopping", false);
                }

                if (Mathf.Abs(rigid.velocity.x) < 0.7)
                {
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    anim.SetBool("isRunning", true);
                }

                Speed = rigid.velocity.x;
                /*if (!anim.GetBool("isJumping") && !damaged)
                {
                    if (curTime <= 0)
                    {
                        if (Input.GetKey(KeyCode.Z))
                        {
                            PlaySound("Attack");

                            anim.SetTrigger("atk");
                            curTime = coolTime;
                        }
                    }
                    else
                    {
                        curTime -= Time.deltaTime;
                    }
                }*/

                if (curTime <= 0)
                {
                    if (Input.GetKey(KeyCode.Z))
                    {
                        if (!anim.GetBool("isJumping") && !damaged)
                        {
                            PlaySound("Attack");

                            anim.SetTrigger("atk");
                            curTime = coolTime;
                        }
                    }
                }

                if (anim.GetBool("isJumping") && !damaged)
                {
                    if (curTime <= 0)
                    {
                        if (Input.GetKey(KeyCode.Z))
                        {
                            PlaySound("JumpAttack");

                            anim.SetTrigger("jumpAtk");
                            curTime = coolTime;
                        }
                    }
                }

                float h = Input.GetAxisRaw("Horizontal");
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

                //최대 속도
                if (rigid.velocity.x > maxSpeed)
                {
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                }
                else if (rigid.velocity.x < maxSpeed * (-1))
                {
                    rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
                }

                //땅에 닿았나 확인
                if (rigid.velocity.y < 0)
                {
                    anim.SetBool("isJumping", true);
                    Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

                    RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.3f, LayerMask.GetMask("Platform"));
                    RaycastHit2D rayHit2 = Physics2D.Raycast(rigid.position, Vector3.down, 1.3f, LayerMask.GetMask("PlatformForPlayer"));
                    if (rayHit.collider != null)
                    {
                        if (rayHit.distance < 3.0f)
                        {
                            anim.SetBool("isJumping", false);
                            isAttack = false;
                        }
                    }
                    if (rayHit2.collider != null)
                    {
                        if (rayHit.distance < 3.0f)
                        {
                            anim.SetBool("isJumping", false);
                            isAttack = false;
                        }
                    }
                }

                RaycastHit2D rayHit3 = Physics2D.Raycast(rigid.position, Vector3.down, 1.3f, LayerMask.GetMask("Platform"));
                if (rayHit3.collider != null)
                {
                    if (rayHit3.collider.gameObject.tag == "PlatformEnemy")
                    {
                        if (!damaged)
                        {
                            OnDamaged(rayHit3.collider.transform.position);
                        }
                    }
                }
            }
            curTime -= Time.deltaTime;

            if (hp <= 2)
            {
                HP2.fillAmount = 0;
                HP3.fillAmount = 0;
                if (hp == 2)
                {
                    HP1.fillAmount = 1f;
                }
                else if (hp == 1)
                {
                    HP1.fillAmount = 0.5f;
                }
                else
                {
                    HP1.fillAmount = 0;
                }
            }
            else if (hp <= 4)
            {
                HP3.fillAmount = 0;
                if (hp == 3)
                {
                    HP2.fillAmount = 0.5f;
                }
                else if (hp == 4)
                {
                    HP2.fillAmount = 1f;
                }
            }
            else
            {
                if (hp == 5)
                {
                    HP3.fillAmount = 0.5f;
                }
                else if (hp > 5)
                {
                    HP3.fillAmount = 1f;
                }
            }
        }
    }

    void AttackStart()
    {
        isAttack = true;
        Invoke("AttackEnd", 1.0f);
    }

    void AttackEnd()
    {
        isAttack = false;
    }

    void AttackPoint()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 12)
            {
                
                if (collider.GetComponent<Enemy>().hp > 0)
                {
                    collider.GetComponent<Enemy>().OnDamaged(transform.position);
                    PlaySound("EnemyAttack");
                }
            }
            if (collider.tag == "Boss")
            {
                
                if (collider.gameObject.layer == 15)
                {
                    if (collider.GetComponent<Malatang>().hp > 0)
                    {
                        collider.GetComponent<Malatang>().OnDamaged();
                        PlaySound("EnemyAttack");
                    }
                }
            }
        }
    }


    public void PlaySound(string name)
    {
        for (int i = 0; i < clip.Length; i++)
        {
            if (name == clip[i].name)
            {
                SoundManager.instance.SFXPlay("Attack", clip[i]);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Thorny")
        {
            OnDamaged(collision.transform.position);
        }
        if (collision.gameObject.tag == "PointItem")
        {
            
            if(collision.GetComponent<Item>().isPoint == false)
            {
                GameManager.instance.score += 800;
                collision.GetComponent<Item>().isPoint = true;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
            PlaySound("OnDamaged");
        }

        if (collision.gameObject.tag == "Boss")
        {
            OnDamaged(collision.transform.position);
            PlaySound("OnDamaged");
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    public void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 13;
        hp -= 1;
        damaged = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int direc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(direc, 1) * 7, ForceMode2D.Impulse);

        anim.SetTrigger("doDamaged");
        Invoke("OffDamaged", 2);
        if(hp <= 0)
        {
            GameManager.instance.gameOver.SetActive(true);
            gameObject.layer = 20;
        }
    }


    void OffDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        damaged = false;
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

}

