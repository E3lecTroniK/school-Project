using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Jump System")]
    [SerializeField] float jumpTime;
    [SerializeField] int jumpPower;
    [SerializeField] float fallMultiplier;
    [SerializeField] float JumpMultiplier;

    public Transform groundCheck;
    public LayerMask GroundLayer;
    bool isGrounded;
    Vector2 vecGravity;

    bool isJumping;
    float jumpcounter;


    void start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
    }
    void update()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.8f, 0.3f), CapsuleDirection2D.Horizontal, GroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpcounter = 0;
        }
        if (rb.velocity.y<0 && isJumping)
       {
            jumpcounter += Time.deltaTime;
            if (jumpcounter > jumpTime) isJumping = false;

            float t = jumpcounter / jumpTime;
            float currentJumpM = JumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = JumpMultiplier * (1 - t);
            }


            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpcounter = 0;

            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }

        }
    
        rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
    }
}