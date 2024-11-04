using UnityEngine;

public class PlayerMovments : MonoBehaviour
{
    float h; // Get Input Horizontal Axis
    public float speed;
    Rigidbody2D rb;
    public float JumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer;

    bool doubleJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) // Jump
        {
            if (isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce * 0.7f);
                doubleJump = false;
            }
        }

        Flip();
    }

       private void FixedUpdate()

        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y); // left - Right Movment
        }

        bool isGrounded() // GroundCheck
        {
            return Physics2D.OverlapBox(groundCheck.position, new Vector2(1.7f, 0.24f), 0, groundLayer);
        }

        void Flip() // Face Direction
        {
            if (h < -0.01f) transform.localScale = new Vector3(-1, 1, 1); if (h > 0.01f)
                if (h > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        }
    }
    

    
              
