using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Match the camera's position to the player's position
        // Follow the player's X and Y position, keep camera's Z position
        transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z);


    }
}
