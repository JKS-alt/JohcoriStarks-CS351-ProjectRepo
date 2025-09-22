using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player1CarMovement : MonoBehaviour
{
    // public float for speed of Player 1 car
    public float Player1CarSpeed;

    // public float for turn speed of player 1 car
    public float Player1CarTurnSpeed;

    // public floats for inputs
    public float HorizontalInput;
    public float VerticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get Player one's car input
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");

        // Move Player forward or back with vertical input
        transform.Translate(Vector2.up * Time.deltaTime * Player1CarSpeed * VerticalInput);

        // if the player car isn't accelerating or reversing, they can't turn
        if (VerticalInput == 0)
        {
            //Nothing happens :(
        }
        else
        {
            // Turn Player left or right with horizontal input
            transform.Rotate(Vector3.back, Player1CarTurnSpeed * Time.deltaTime * HorizontalInput);
        }


    }
}
