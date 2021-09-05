using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    public float speed;
    public float y;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - Time.deltaTime * PlayerMove.instance.Speed * speed * 0.1f, y, transform.position.z);
        
    }
}
