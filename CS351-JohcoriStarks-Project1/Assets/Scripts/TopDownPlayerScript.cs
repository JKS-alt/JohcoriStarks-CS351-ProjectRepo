using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerScript : MonoBehaviour
{
    //adjust this value in the inspector to set the player movement speed
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        // get the ridgid bod
    rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // get movement values
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // normalise the movemnt vector to prevent dialognal movemnt'
        movement.Normalize();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement .x * moveSpeed, movement .y * moveSpeed);
    }
}
