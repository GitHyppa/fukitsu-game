using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject player;


    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //Get the value of the Horizontal input axis.

        float verticalInput = Input.GetAxis("Vertical");
        //Get the value of the Vertical input axis.
     
        Move(player, horizontalInput, verticalInput);
    }

    void Move(GameObject player, float horizontalInput, float verticalInput)
    {
        if(Input.GetKey("d"))
        {
            player.transform.position = new Vector3(player.transform.position.x + 0.05f, player.transform.position.y, player.transform.position.z);
        }

        if (Input.GetKey("a"))
        {
            player.transform.position = new Vector3(player.transform.position.x - 0.05f, player.transform.position.y, player.transform.position.z);
        }

        if (Input.GetKey("w"))
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.05f);
        }

        if (Input.GetKey("s"))
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 0.05f);
        }
    }
}
