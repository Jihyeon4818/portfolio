using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(enemy == null)
        {
            this.gameObject.SetActive(true);
        }
    }
}
