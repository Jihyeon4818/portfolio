using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Candy : MonoBehaviour
{
    public bool ready = true;
    public string moveMapName; // 이동할 맵 이름
    public GameObject stageClear;
    Animator anim;
    float playerDistance;
    bool isFade;
    public int mapScore;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, PlayerMove.instance.transform.position);
        if (playerDistance < 7.0f)
        {
            anim.SetBool("isFind", true);
        }
        if(isFade)
        {
            Color color = GameManager.instance.fade.color;
            if (color.a < 1)
            {
                color.a += Time.deltaTime * 0.3f;
            }
            GameManager.instance.fade.color = color;
        }
        else
        {
            Color color = GameManager.instance.fade.color;
            if (color.a > 0)
            {
                color.a -= Time.deltaTime * 0.3f;
            }
            else
            {
                ready = true;
            }
            GameManager.instance.fade.color = color;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetBool("isSave", true);
            gameObject.layer = 11;
            isFade = true;
            GameManager.instance.gameUI.SetActive(false);
            PlayerMove.instance.uiStart = false;
            PlayerMove.instance.anim.SetBool("isRunning", false);
            ready = false;
            stageClear.SetActive(true);
            Invoke("MoveMap", 4);
        }
    }
    void MoveMap()
    {
        GameManager.instance.score += mapScore;
        SceneManager.LoadScene(moveMapName);
    }
}
