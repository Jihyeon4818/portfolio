using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveCamera : MonoBehaviour
{
    public GameObject stage1Background;
    public GameObject stage2Background;
    public GameObject stage3Background;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    static public moveCamera instance;




    public float speed;
    Vector3 fixPosition;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            height = Camera.main.orthographicSize;
            width = height * Screen.width / Screen.height;
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        else
        {
            Destroy(this.gameObject);
        }
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;


    }


    void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "2-1Stage")
        {
            center = new Vector2(71.5f, 7);
            size = new Vector2(169, 18);
            stage1Background.SetActive(true);
            stage2Background.SetActive(false);
            stage3Background.SetActive(false);
            stage1.SetActive(true);
            stage2.SetActive(false);
            stage3.SetActive(false);
        }
        else if (arg0.name == "2-2Stage")
        {
            center = new Vector2(75, 7);
            size = new Vector2(178, 18);
            stage1Background.SetActive(false);
            stage2Background.SetActive(true);
            stage3Background.SetActive(false);
            stage1.SetActive(false);
            stage2.SetActive(true);
            stage3.SetActive(false);
        }
        else if (arg0.name == "2-3Stage")
        {
            center = new Vector2(99.5f, 7);
            size = new Vector2(227, 18);
            stage1Background.SetActive(false);
            stage2Background.SetActive(false);
            stage3Background.SetActive(true);
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(true);
        }
        else if (arg0.name == "1MainMenu")
        {
            center = new Vector2(-2.4f, 4);
            size = new Vector2(21.7f, 12.2f);
            stage1Background.SetActive(false);
            stage2Background.SetActive(false);
            stage3Background.SetActive(false);
        }
        else if (arg0.name == "3Epilogue")
        {
            center = new Vector2(-2.4f, 4);
            size = new Vector2(21.7f, 12.2f);
            stage1Background.SetActive(true);
            stage2Background.SetActive(false);
            stage3Background.SetActive(false);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void Update()
    {
        //rigidSpeed = rigid.velocity.x;
        //Debug.Log(rigidSpeed);
    }

    void LateUpdate()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, -10f);
        if (PlayerMove.instance != null)
        {
            fixPosition = new Vector3(PlayerMove.instance.transform.position.x + 1.5f, PlayerMove.instance.transform.position.y + 1.5f, -15f);
            transform.position = Vector3.Lerp(transform.position, fixPosition, Time.deltaTime * speed);
            float lx = size.x * 0.5f - width;
            float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
            float ly = size.y * 0.5f - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

            transform.position = new Vector3(clampX, clampY, -15f);
        }
    }
}
