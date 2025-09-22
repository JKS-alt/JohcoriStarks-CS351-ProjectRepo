using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerScript : MonoBehaviour
{

    // Try setting this to 8 in the Inspector
    public float speed;

    // Try setting this to 100 in the Inspector
    public float turnSpeed;

    private float horizontalInput;
    private float verticalInput;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input each frame
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move player forward/backward based on vertical input
        transform.Translate(Vector3.right * Time.deltaTime * verticalInput * speed);

        // Rotate player based on horizontal input
        transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime * horizontalInput);

        if (verticalInput < 0)
        {
            transform.Rotate(Vector3.back, -turnSpeed * Time.deltaTime * horizontalInput);
        }
        else
        {
            transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime * horizontalInput);
        }

    }



}
