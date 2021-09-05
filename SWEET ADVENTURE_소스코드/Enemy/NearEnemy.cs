using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnemy : Enemy
{
    

    public Transform pos;
    public Vector2 boxSize;
    
    public GameObject playerHitEffect;

    

    protected void FixedUpdate()
    {
        Vector2 frontVect = new Vector2(rigid.position.x + chiliMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVect, Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(frontVect, new Vector3(chiliMove,0,0)*8, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVect, Vector3.down, 2, LayerMask.GetMask("Platform"));
        RaycastHit2D CheckPlayer = Physics2D.Raycast(frontVect, new Vector3(chiliMove, 0, 0),8, LayerMask.GetMask("Player"));
        if (CheckPlayer.collider != null)
        {
            isPerception = true;
        }
        else
        {
            isPerception = false;
        }
        
        if (!damaged)
        {
            if (!isAttack)
            {
                rigid.velocity = new Vector2(chiliMove * 2, rigid.velocity.y);
                anim.SetBool("isWalking", true);
            }
            if (isPerception)
            {
                if (Vector2.Distance(PlayerMove.instance.transform.position, transform.position) < atkDistance)
                {
                    if (curTime <= 0)
                    {
                        anim.SetTrigger("atk");
                        curTime = coolTime;

                    }
                    ShowPlayer();
                    rigid.velocity = Vector2.zero;
                    anim.SetBool("isWalking", false);
                }
                else if(!isAttack)
                {
                    Follow();
                }

            }
            


            if (rayHit.collider == null)
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

        }
        curTime -= Time.deltaTime;

    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }


    protected void AttackPoint()
    {
        
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 9)
            {
                collider.GetComponent<PlayerMove>().OnDamaged(transform.position);
                PlayerMove.instance.PlaySound("OnDamaged");
                PlayerHitEffect();
            }
        }        
    }

    public void PlayerHitEffect()
    {
        Instantiate(playerHitEffect, new Vector3(PlayerMove.instance.transform.position.x, PlayerMove.instance.transform.position.y, -11), PlayerMove.instance.transform.rotation);
    }

    
}
