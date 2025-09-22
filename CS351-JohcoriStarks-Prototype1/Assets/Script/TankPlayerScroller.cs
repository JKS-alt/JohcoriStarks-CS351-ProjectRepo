using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerScroller : MonoBehaviour
{
    //Try setting this to 8 in the inpector
    public float speed;

    // a float for turn speed
    public float TurnSpeed;

    public float horizontalInput;
    public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moving foward
        //transform.Translate(1,0,0);

        // transform.Translate(Vector3.right * Time.deltaTime * speed);

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move forward/backward
        transform.Translate(Vector3.right * Time.deltaTime * speed * verticalInput);

        // Rotate left/right
        transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime * horizontalInput);

        transform.Rotate(Vector3.back, TurnSpeed * Time.deltaTime * speed * verticalInput * horizontalInput);


    }

}

