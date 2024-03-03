using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndDoor : MonoBehaviour
{
    private bool playerInDoor;
    private SpriteRenderer doorSprite;
    private stateFlip isExit;

    // Start is called before the first frame update
    void Start()
    {
        playerInDoor = false;
        doorSprite = gameObject.GetComponent<SpriteRenderer>();
        isExit = GetComponent<stateFlip>();
        isExit.setState(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Changes color of door depending on if it is an entrance or exit
        if (isExit.getState())
        {
            // ... if it is an exit, become red
            doorSprite.color = new Color32(140, 65, 70, 255);
        } 
        else
        {
            // ... if it is an entrance, become green
            doorSprite.color = new Color32(72, 140, 64, 255);
        }
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
}
