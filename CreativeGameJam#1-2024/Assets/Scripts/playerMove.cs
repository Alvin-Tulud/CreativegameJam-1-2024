using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerMove : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    bool canJumpNow;
    public float jumpForce;
    public float speed;
    public LayerMask jumpableSurface;

    public Sprite[] states;

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
        if(!hit)
        {
            canJumpNow = false;

        } else
        {
            canJumpNow = true;
        }


        //left right
        Vector2 horizontalMovement = Vector2.zero;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } 


        //jumping
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (canJumpNow)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }


        if(rb.velocity.magnitude == 0)
        {
            GetComponent<SpriteRenderer>().sprite = states[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = states[1];
        }
    }
}
