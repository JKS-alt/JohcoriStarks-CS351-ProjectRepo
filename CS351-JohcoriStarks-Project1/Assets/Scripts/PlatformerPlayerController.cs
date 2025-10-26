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
    private Animator animator;
    private Vector3 originalScale;
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }
        originalScale = transform.localScale;

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
            animator.SetTrigger("Jump");
            jumpBufferCounter = jumpBufferTime;
        }

        jumpBufferCounter -= Time.deltaTime;

    }

    void FixedUpdate()
    {
        // move the player using ridgid body 2d in fixed update
        rb.velocity = new Vector2(HorizontalInput * MoveSpeed, rb.velocity.y);
       
        // Check if the player is grounded
        // Check if the player is grounded FIRST
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Delay ground check briefly after jumping
        if (jumpBufferCounter <= 0f)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);


        if (HorizontalInput > 0)
        {
            transform.localScale = new Vector3(12f, 12f, 1f);
        }
        else if (HorizontalInput < 0)
        {
            transform.localScale = new Vector3(-12f, 12f, 1f);
        }
        animator.SetFloat("x velocity", Mathf.Abs(rb.velocity.x));
        
        if (HorizontalInput != 0f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

       
    }
}
