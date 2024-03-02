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
    void Update()
    {
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
            if (jump.GetComponent<canJump>().jumpState())
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
