using System;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{   
    private Animator animator;
    private float moveSpeed = 1000f;
    private float jumpForce = 300f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isGrounded;

    public Transform groundedLeft;
    public Transform groundedRight;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundedLeft.position, groundedRight.position);
        float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalInput);
        Flip(rb.linearVelocityX);
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    void MovePlayer(float _horizontalInput){
        Vector3 targetVelocity = new Vector2(_horizontalInput, rb.linearVelocityY);
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));

        if(isJumping == true && isGrounded == true){
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
    void Flip(float velocity)
    {
        if (velocity > 1)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (velocity < -1)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

}
