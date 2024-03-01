using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffPlatformState : MonoBehaviour
{
    public SpriteRenderer platformSprite;
    public BoxCollider2D platformCollider;
    public bool initialPlatformState;
    private bool currentPlatformState;


    // Start is called before the first frame update
    void Start()
    {
        currentPlatformState = initialPlatformState;
        platformSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determines whether the platform is active or not
        if (currentPlatformState) 
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

    public void changePassthroughState()
    {
        currentPlatformState = !currentPlatformState;
    }
}
