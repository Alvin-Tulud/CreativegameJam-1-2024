using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    canJump jump;
    public float jumpForce;
    public float speed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jump = GetComponentInChildren<canJump>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //left right
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = speed * Vector2.left;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = speed * Vector2.right;
        }

        //jumping
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            if (jump.GetComponent<canJump>().jumpState())
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
