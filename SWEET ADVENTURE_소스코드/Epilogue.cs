using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Epilogue : MonoBehaviour
{
    protected Rigidbody2D rigid;
    public float speed;
    bool isFade;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Fade", 84);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(speed , rigid.velocity.y);
        if(!isFade)
        {
            Color color = GameManager.instance.fade.color;
            if (color.a > 0)
            {
                color.a -= Time.deltaTime * 0.2f;
            }
            GameManager.instance.fade.color = color;

        }
        if (isFade)
        {
            Color color = GameManager.instance.fade.color;
            if (color.a < 1)
            {
                color.a += Time.deltaTime * 0.2f;
            }
            GameManager.instance.fade.color = color;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Fade();
        }
    }

    void Fade()
    {
        isFade = true;
        Invoke("MainMenu", 4);
    }

    void MainMenu()
    {
        Color color  = GameManager.instance.fade.color;
        color.a = 0;
        
        SceneManager.LoadScene("1MainMenu");
        GameManager.instance.fade.color = color;
    }
}
