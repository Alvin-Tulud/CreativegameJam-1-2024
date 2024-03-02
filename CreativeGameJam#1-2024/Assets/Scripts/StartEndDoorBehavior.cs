using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndDoor : MonoBehaviour
{
    private bool PlayerTrigger;
    private bool isExit;
    private SpriteRenderer doorSprite;
    private stateFlip stateFlip;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTrigger = false;
        isExit = false;
        doorSprite = gameObject.GetComponent<SpriteRenderer>();
        stateFlip = GetComponent<stateFlip>();
        stateFlip.setState(isExit);
    }

    // Update is called once per frame
    void Update()
    {
        isExit = stateFlip.getState();

        // Changes color of door depending on if it is an entrance or exit
        if (isExit)
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
        if (collision.transform.CompareTag("Player") && isExit)
        {
            PlayerTrigger = true;
        }
    }
    public bool getPlayerTrigger() { return PlayerTrigger; }
}
