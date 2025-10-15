// Author: Johcori Starks
// date 9/22/2025
// Description controll plat former player
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private Rigidbody2D rb;
    private float HorizontalInput;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public bool isGrounded;
    public AudioClip JumpSound;
    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {


        HorizontalInput = Input.GetAxis("Horizontal");

        // Check if the player is grounded
       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAudio.PlayOneShot(JumpSound, 1.0f);
        }


        
    }

     void FixedUpdate()
    {
        // move the player using ridgid body 2d in fixed update
        rb.velocity =new Vector2(HorizontalInput * MoveSpeed, rb.velocity.y);

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (HorizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (HorizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
