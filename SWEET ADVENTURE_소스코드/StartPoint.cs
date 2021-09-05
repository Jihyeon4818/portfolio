using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    PlayerMove player;
    moveCamera myCamera;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
        myCamera = FindObjectOfType<moveCamera>();
        myCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, myCamera.transform.position.z);
        if (PlayerMove.instance == null)
        {
            player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z);
            
        }
        else
        {
            PlayerMove.instance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, PlayerMove.instance.transform.position.z);
        }
    }

}
