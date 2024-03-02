using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position + -transform.right * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * speed);
        }
        else if (Input.GetKey(KeyCode.W) ||  Input.GetKey(KeyCode.Space))
        {
            rb.MovePosition(transform.position + transform.up * speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position + -transform.up * speed);
        }
    }
}
