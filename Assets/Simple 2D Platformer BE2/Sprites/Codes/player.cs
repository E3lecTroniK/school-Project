using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jump;
    public float groundedY;
    public LedgeGrab ledgeGrab;
    public Animations animations;

    bool controls = true;
    bool dead;
    Vector2 startPosition;

    Animator animator;
    new Rigidbody2D rigidbody;

    private void Awake()
    {

        animator = GetComponent<Animator>();

        rigidbody = GetComponent<Rigidbody2D>();

        startPosition = transform.position;

    }

    void Update()
    {

        if (controls)
        {

            // Move
            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

            CheckJump();

            CheckLedgeGrab();

        }

        CheckAnimations();

    }

    void CheckAnimations()
    {

        if (IsJumpFinished() && !dead)
        {

            if (Input.GetAxis("Horizontal") < 0)
            {

                animator.Play(animations.goLeft.name);

            }
            else if (Input.GetAxis("Horizontal") > 0)
            {

                animator.Play(animations.goRight.name);

            }
            else
            {

                animator.Play("Idle");

            }

        }

    }

    void CheckJump()
    {

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {

            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jump, ForceMode2D.Impulse);

            animator.Play(animations.jumpRight.name);

            if (Input.GetAxis("Horizontal") < 0) { animator.Play(animations.jumpLeft.name); }

        }

    }

    void CheckLedgeGrab()
    {

        float rayStartOriented = ledgeGrab.rayStart;

        Vector2 orientation = Vector2.right;

        Vector3 targetOriented = ledgeGrab.target;

        if (Input.GetAxis("Horizontal") < 0)
        {

            rayStartOriented = -ledgeGrab.rayStart;

            orientation = -orientation;

            targetOriented.x = -targetOriented.x;

        }


        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + new Vector3(rayStartOriented, ledgeGrab.ray1), orientation, ledgeGrab.rayLength);

        if (hit1.collider != null) { return; }

        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(rayStartOriented, ledgeGrab.ray2), orientation, ledgeGrab.rayLength);

        if (hit2.collider == null) { return; }

        if (!Input.GetButton("Jump")) { return; }


        StartCoroutine(LedgeGrabRoutine(targetOriented));

    }

    public IEnumerator LedgeGrabRoutine(Vector3 targetPosition)
    {

        // Play your animation

        rigidbody.velocity = Vector2.zero;

        rigidbody.gravityScale = 0;

        controls = false;


        Vector3 targetPositionWorld = transform.position + targetPosition;

        while (transform.position.y < targetPositionWorld.y)
        {

            transform.Translate(Vector2.up * Time.deltaTime * ledgeGrab.speed);

            yield return null;

        }

        while (targetPosition.x < 0 && transform.position.x > targetPositionWorld.x || targetPosition.x > 0 && transform.position.x < targetPositionWorld.x)
        {

            if (targetPosition.x <= 0) { transform.Translate(Vector2.left * Time.deltaTime * ledgeGrab.speed); }

            if (targetPosition.x > 0) { transform.Translate(Vector2.right * Time.deltaTime * ledgeGrab.speed); }

            yield return null;

        }

        rigidbody.velocity = Vector2.zero;

        rigidbody.gravityScale = 1;

        controls = true;

    }

    public void Die()
    {

        animator.Play(animations.die.name, 0, 0);

        dead = true;

    }

    public void Respawn()
    {

        transform.position = startPosition;

        dead = false;

    }

    public bool IsGrounded()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, groundedY), Vector2.down, .1f);

        if (hit.collider != null)
        {

            return true;

        }

        return false;

    }

    public bool IsJumpFinished()
    {

        if (!IsGrounded()) { return false; }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump")) { return true; }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < animator.GetCurrentAnimatorStateInfo(0).length) { return false; }

        return true;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + new Vector3(0, groundedY), Vector2.down * .1f);

        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position + new Vector3(ledgeGrab.rayStart, ledgeGrab.ray1), Vector2.right * ledgeGrab.rayLength);

        Gizmos.DrawRay(transform.position + new Vector3(ledgeGrab.rayStart, ledgeGrab.ray2), Vector2.right * ledgeGrab.rayLength);

    }


    [System.Serializable]
    public struct LedgeGrab
    {

        public float ray1;
        public float ray2;
        public float rayStart;
        public float rayLength;
        public Vector3 target;
        public float speed;

    }

    [System.Serializable]
    public struct Animations
    {

        public AnimationClip goLeft;
        public AnimationClip goRight;
        public AnimationClip jumpLeft;
        public AnimationClip jumpRight;
        public AnimationClip die;

    }

}
