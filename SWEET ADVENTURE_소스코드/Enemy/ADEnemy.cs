using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADEnemy : Enemy
{
    public GameObject projectileObj;



    protected void FixedUpdate()
    {
        Vector2 frontVect = new Vector2(rigid.position.x + chiliMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVect, Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(frontVect, new Vector3(chiliMove, 0, 0) * 12, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVect, Vector3.down, 2, LayerMask.GetMask("Platform"));
        RaycastHit2D CheckPlayer = Physics2D.Raycast(frontVect, new Vector3(chiliMove, 0, 0), 12, LayerMask.GetMask("Player"));
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
                else if (!isAttack)
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

    protected void AttackPoint()
    {
        GameObject projectile = Instantiate(projectileObj, transform.position, transform.rotation);
        Rigidbody2D PTrigid = projectile.GetComponent<Rigidbody2D>();
        Vector2 direct = (PlayerMove.instance.transform.position - transform.position).normalized;
        projectile.transform.right = direct;
        PTrigid.AddForce(direct * 7, ForceMode2D.Impulse);
    }
}
