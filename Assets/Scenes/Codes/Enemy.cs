using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    public float raydist;
    private bool movingright;
    public Transform groundDetect;
    public LayerMask groundlayer;


    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, raydist ,groundlayer);

        if (groundCheck.collider == false)

            if(movingright)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingright = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingright = true;
        }

    }
}
