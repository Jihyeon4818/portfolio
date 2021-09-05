using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Transform cameraTrans;
    public float speed;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * 0.5f, cameraTrans.position.y+3.6f, transform.position.z);

        if (transform.position.x - cameraTrans.position.x > distance)
        {
            transform.position = new Vector3(cameraTrans.position.x - distance, cameraTrans.position.y + 3.6f, transform.position.z);
        }
        else if (transform.position.x - cameraTrans.position.x < -distance)
        {
            transform.position = new Vector3(cameraTrans.position.x + distance, cameraTrans.position.y + 3.6f, transform.position.z);
        }
    }
}
