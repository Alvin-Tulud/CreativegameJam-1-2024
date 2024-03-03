using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSwitch : MonoBehaviour
{
    private bool isPushedDown;              
    private SpriteRenderer buttonSprite;

    public GameObject[] coins;              //Holds the list of coins in the scene for the switch to affect

    //Animation related
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    Animator animator;
    string currentState;
    const string BUTTON_DOWN = "Button_Down";
    const string BUTTON_UP = "Button_Up";
    const string BUTTON_PRESS = "Button_Press";
    const string BUTTON_RELEASE = "Button_Release";

    // Start is called before the first frame update
    void Start()
    {
        isPushedDown = false;
        buttonSprite = gameObject.GetComponent<SpriteRenderer>();

        animator = gameObject.GetComponent<Animator>();

        ChangeAnimationState(BUTTON_UP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the button is pressed:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FlipCoins();
        isPushedDown = !isPushedDown;
        ChangeAnimationState(BUTTON_PRESS);

        /*Changes color depending on flip status. Uncomment to use for debugging
        if (isPushedDown)
        {
            buttonSprite.color = new Color32(32, 67, 144, 255);
        }
        else
        {
            buttonSprite.color = new Color32(38, 218, 243, 255);
        }*/
    }

    //When the player leaves the button:
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Release button animation:
        //Disable collider
        BoxCollider2D switchCollider = gameObject.GetComponent<BoxCollider2D>();
        switchCollider.enabled = false;

        //Wait a few seconds
        //Enable collider
        StartCoroutine(ButtonRelease(switchCollider));
    }

    //Releases the button: After a few seconds, reenables collider + plays release animation
    //Takes the box collider as a parameter so I don't have to redeclare
    IEnumerator ButtonRelease(BoxCollider2D switchCollider)
    {
        //The delay before the button is released:
        yield return new WaitForSeconds(0.8f);

        //Release animation
        ChangeAnimationState(BUTTON_RELEASE);

        //Reenable collider so the button can be pressed again
        switchCollider.enabled = true;
    }

    private void FlipCoins()
    {
        //Need to re-get the list of coins since some may have been picked up
        coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject w in coins)
        {
            //flip state of each one
            w.GetComponent<coinScript>().CoinSwitch();
        }

    }


    //Animation related
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    private void ChangeAnimationState(string newState)
    {
        if(newState == currentState)
        {
            return;
        }

        animator.Play(newState);

        currentState = newState;
    }
}
