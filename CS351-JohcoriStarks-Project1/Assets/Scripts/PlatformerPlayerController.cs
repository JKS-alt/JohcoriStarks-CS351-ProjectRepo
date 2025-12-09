// Author: Johcori Starks
// date 9/22/2025
// Description controll plat former player

using System.Collections;
using System.Collections.Generic;
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
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PlayerHealth.hitRecently)
        {
            animator.SetBool("IsRunning", false);
            return;
        }

        HorizontalInput = Input.GetAxis("Horizontal");

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
        if (PlayerHealth.hitRecently)
            return;

        rb.velocity = new Vector2(HorizontalInput * MoveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (jumpBufferCounter <= 0f)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);

        if (HorizontalInput > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (HorizontalInput < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        animator.SetBool("IsRunning", HorizontalInput != 0f);
    }
}
