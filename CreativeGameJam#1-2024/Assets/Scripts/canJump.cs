using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class canJump : MonoBehaviour
{
    // Start is called before the first frame update
    bool onGround;
    bool canJumpNow;

    void Start()
    {
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            canJumpNow = true;
        }
        else
        {
            canJumpNow = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            onGround = false;
        }
    }

    public bool jumpState() { return canJumpNow; }
}
