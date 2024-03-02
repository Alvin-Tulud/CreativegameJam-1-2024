using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class canJump : MonoBehaviour
{
    // Start is called before the first frame update
    bool canJumpNow;
    public LayerMask jumpableSurface;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, jumpableSurface);
        Debug.Log(hit.transform.name);
        if (hit.transform.CompareTag("Wall"))
        {
            canJumpNow = true;
        }
        else
        {
            canJumpNow = false;
        }
    }

    public bool jumpState() { return canJumpNow; }
}
