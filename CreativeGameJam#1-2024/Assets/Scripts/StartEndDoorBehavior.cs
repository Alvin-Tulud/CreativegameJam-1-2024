using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class StartEndDoor : MonoBehaviour
{
    private bool playerInDoor;
    private SpriteRenderer doorSprite;
    private stateFlip isExit;

    //Animation related
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    Animator animator;
    string currentState;
    const string DOOR_OPEN = "Door_Open";
    const string DOOR_CLOSED = "Door_Closed";
    const string DOOR_OPENING = "Door_Opening";
    const string DOOR_CLOSING = "Door_Closing";


    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        playerInDoor = false;
        doorSprite = gameObject.GetComponent<SpriteRenderer>();
        isExit = GetComponent<stateFlip>();
        isExit.setState(false);

        //Defines the animator and sets the door to be closed
        animator = gameObject.GetComponent<Animator>();
        ChangeAnimationState(DOOR_CLOSED);


        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        var emission = ps.emission;
        emission.enabled = isExit.getState();
    }

    // Allows PlayerTrigger to change, only when the door is an exit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && isExit.getState())
        {
            playerInDoor = true;
        }
    }
    public bool getPlayerTrigger() { return playerInDoor; }

    //Animation related
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    public void ChangeAnimationState(string newState)
    {
        if (newState == currentState)
        {
            return;
        }

        animator.Play(newState);

        currentState = newState;
    }
}
