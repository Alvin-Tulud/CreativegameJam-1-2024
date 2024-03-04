
using UnityEngine;

public class canJump : MonoBehaviour
{
    // Start is called before the first frame update
    bool canJumpNow;
    public LayerMask jumpableSurface;

    // Update is called once per frame
    void Update()
    {
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
    }

    public bool jumpState() { return canJumpNow; }
}
