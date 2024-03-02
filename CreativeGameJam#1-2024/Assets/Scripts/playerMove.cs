using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    bool canJumpNow;
    public float jumpForce;
    public float speed;
    public LayerMask jumpableSurface;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ground dectection
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, jumpableSurface);
        if (!hit)
        {
            canJumpNow = false;
        }
        else
        {
            canJumpNow = true;
        }


        //left right
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * speed;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * speed;
        }

        //jumping
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (canJumpNow)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
