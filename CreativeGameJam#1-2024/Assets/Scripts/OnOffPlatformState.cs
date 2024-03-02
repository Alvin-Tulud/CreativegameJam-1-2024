using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffPlatformState : MonoBehaviour
{
    public bool initialPlatformState;
    private SpriteRenderer platformSprite;
    private BoxCollider2D platformCollider;
    private stateFlip isSolid;


    // Start is called before the first frame update
    void Start()
    {
        platformSprite = gameObject.GetComponent<SpriteRenderer>();
        platformCollider = gameObject.GetComponent<BoxCollider2D>();

        isSolid = gameObject.GetComponent<stateFlip>();
        isSolid.setState(initialPlatformState);
    }

    // Update is called once per frame
    void Update()
    {

        // Determines whether the platform is active or not
        if (isSolid.getState()) 
        {
            // .. if it is the case, make it collidable and change color to active
            platformCollider.enabled = true;
            platformSprite.color = Color.white;
        } 
        else 
        {
            // ... if not the case, make it passthrough and change color to in active
            platformCollider.enabled = false;
            platformSprite.color = Color.grey;
        }
    }
}
