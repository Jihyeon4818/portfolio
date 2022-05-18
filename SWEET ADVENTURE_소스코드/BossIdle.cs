using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : MonoBehaviour
{
    //moveCamera myCamera;

    BossPlatform bossPlatform;
    public GameObject boss;
    public GameObject bossUI;
    public AudioClip bossClip;
    public GameObject unitImage;
    public GameObject enemy;
    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        //myCamera = FindObjectOfType<moveCamera>();

        bossPlatform = FindObjectOfType<BossPlatform>();
        bossPlatform.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Color color = GameManager.instance.fade.color;
        if (color.a > 0)
        {
            color.a -= Time.deltaTime * 0.3f;
        }
        GameManager.instance.fade.color = color;
        if(Vector2.Distance(transform.position,PlayerMove.instance.transform.position) <= 10)
        {
            //myCamera.center = new Vector2(215, 7);
            //myCamera.size = new Vector2(10, 18);
            enemy.SetActive(false);
            moveCamera.instance.transform.position = new Vector3(220.5f, 4f, -15);
            moveCamera.instance.on = false;
            Instantiate(boss, transform.position, transform.rotation);
            PlayerMove.instance.transform.position = new Vector3(211.8f, 0.4f, -10);
            PlayerMove.instance.uiStart = false;
            PlayerMove.instance.horizontal = 0;
            PlayerMove.instance.Speed = 0;
            PlayerMove.instance.anim.SetBool("isRunning", false);
            bossPlatform.gameObject.SetActive(true);
            soundManager.BGSoundPlay(bossClip);
            unitImage.SetActive(false);
            GameManager.instance.bossHPImage.SetActive(true);
            GameManager.instance.unit.SetActive(false);
            GameManager.instance.gameUI.SetActive(false);
            bossUI.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}
