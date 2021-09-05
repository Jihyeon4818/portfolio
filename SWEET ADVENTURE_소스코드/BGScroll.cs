using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private MeshRenderer render;
    PlayerMove player;
    public float speed;
    private float offset;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed*player.Speed;
        render.material.mainTextureOffset = new Vector2(offset, 0);
        //transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
