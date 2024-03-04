
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
        int xMovement = 0;
        int yMovement = 0;


        if (Input.GetKey(KeyCode.A))
        {
            xMovement = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xMovement = 1;
        }

        if (Input.GetKey(KeyCode.W) ||  Input.GetKey(KeyCode.Space))
        {
            yMovement = 1; 
        }
        else if (Input.GetKey(KeyCode.S))
        {
            yMovement = -1;
        }
        
        rb.MovePosition(new Vector2(transform.position.x + (speed * xMovement), transform.position.y + (speed * yMovement)));
    }
}
