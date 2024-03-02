using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class canJump : MonoBehaviour
{
    // Start is called before the first frame update
    bool canJumpNow;
    public LayerMask jumpableSurface;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, jumpableSurface);
        if(!hit)
        {
            canJumpNow = false;
        }
        else if (hit.transform.CompareTag("Wall"))
        {
            canJumpNow = true;
        }
    }

    public bool jumpState() { return canJumpNow; }
}
