using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public GameObject gameOverbtn;
    public GameObject gameOver;
    public GameObject gameUI;
    public Enemy[] allEnemy;
    Malatang boss;
    public Text scoreText;
    public Text enemyNum;
    public Text bossHP;
    public GameObject unit;
    public GameObject bossHPImage;
    public Image fade;

    public GameObject stage1Mountain;
    public GameObject stage2Mountain;
    public GameObject stage3Mountain;

    public int score;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        score = 0;
    }

    private void Update()
    {

        scoreText.text = score.ToString();
        allEnemy = FindObjectsOfType<Enemy>();
        enemyNum.text = allEnemy.Length.ToString();
        boss = FindObjectOfType<Malatang>();
        if (boss != null)
        {
            bossHP.text = boss.hp.ToString();
            if (boss.isDead)
            {
                Color color = fade.color;
                if (color.a < 1)
                {
                    color.a += Time.deltaTime*0.2f;
                }
                fade.color = color;

            }
        }
    }

    // 추락 판정
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerMove.instance.hp > 1)
            {
                PlayerRePosition();
            }
            PlayerMove.instance.OnDamaged(transform.position);
        }
    }

    void PlayerRePosition()
    {
        PlayerMove.instance.transform.position = new Vector3(-10.3f, 0, PlayerMove.instance.transform.position.z);
        PlayerMove.instance.VelocityZero();
        moveCamera.instance.transform.position = new Vector3(-2.39f, 4.072773f, moveCamera.instance.transform.position.z);
        stage1Mountain.transform.position = new Vector3(7.786785f, 1.344044f, stage1Mountain.transform.position.z);
        stage2Mountain.transform.position = new Vector3(9.312808f, 1.474873f, stage1Mountain.transform.position.z);
        stage3Mountain.transform.position = new Vector3(10.9082f, 1.400113f, stage1Mountain.transform.position.z);
        

    }

    public void ReStartBtn()
    {

        ReStart();

        SceneManager.LoadScene("1MainMenu");

    }
    public void ReStart()
    {
        PlayerMove.instance.hp = 6;
        score = 0;
        PlayerMove.instance.VelocityZero();
        moveCamera.instance.transform.position = new Vector3(-2.39f, 4.072773f, moveCamera.instance.transform.position.z);
        moveCamera.instance.on = true;
        stage1Mountain.transform.position = new Vector3(7.786785f, 1.344044f, stage1Mountain.transform.position.z);
        stage2Mountain.transform.position = new Vector3(9.312808f, 1.474873f, stage1Mountain.transform.position.z);
        stage3Mountain.transform.position = new Vector3(10.9082f, 1.400113f, stage1Mountain.transform.position.z);
        PlayerMove.instance.HP1.fillAmount = 1f;
        PlayerMove.instance.HP2.fillAmount = 1f;
        PlayerMove.instance.HP3.fillAmount = 1f;
        PlayerMove.instance.uiStart = false;
        PlayerMove.instance.anim.SetBool("isRunning", false);
        PlayerMove.instance.anim.SetBool("isJumping", false);
        bossHPImage.SetActive(false);
        unit.SetActive(true);
        gameOverbtn.SetActive(false);
        gameOver.SetActive(false);
        gameUI.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayerBuff(float maxSpeed, float coolTime, Vector2 boxSize)
    {
        PlayerMove.instance.maxSpeed = maxSpeed;
        PlayerMove.instance.coolTime = coolTime;
        PlayerMove.instance.boxSize = boxSize;
        //PlayerMove.instance.attackBox.transform.position = boxPos;
    }
}
