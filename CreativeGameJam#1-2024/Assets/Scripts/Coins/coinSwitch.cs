using System.Collections;
using UnityEngine;

public class coinSwitch : MonoBehaviour
{
    public bool isPushedDown;
    private SpriteRenderer buttonSprite;
    BoxCollider2D switchCollider;

    public GameObject[] coins;              //Holds the list of coins in the scene for the switch to affect
    public GameObject[] coinSwitches;       //Holds the list of other switches to flipflop with this one

    //Animation related
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    Animator animator;
    string currentState;
    const string BUTTON_DOWN = "Button_Down";
    const string BUTTON_UP = "Button_Up";
    const string BUTTON_PRESS = "Button_Press";
    const string BUTTON_RELEASE = "Button_Release";


    AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        buttonSprite = gameObject.GetComponent<SpriteRenderer>();
        switchCollider = gameObject.GetComponent<BoxCollider2D>();  //Collider to disable/enable

        //Gather all other coinSwitches to flip with this one
        coinSwitches = GameObject.FindGameObjectsWithTag("CoinSwitch");


        //Defines the animator and sets the button to starting position
        animator = gameObject.GetComponent<Animator>();
        if (isPushedDown)
        {
            ChangeAnimationState(BUTTON_DOWN);
        }
        else
        {
            ChangeAnimationState(BUTTON_UP);
        }



        buttonSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //When the button is pressed:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Unpress all pressed buttons, press all unpressed buttons
        foreach (GameObject w in coinSwitches)
        {
            coinSwitch s = w.GetComponent<coinSwitch>();
            if(s.isPushedDown == true)
            {   //Unpress if pressed
                s.UnpressButton();
            }
            else
            {   //Press if unpressed
                s.PressButton();
            }
            
        }

        //Effect and sound can't be in the press function or all the buttons will play it
        FlipCoins();
        buttonSound.Play();


    }

    //Presses down the button and disables until unpressed
    private void PressButton()
    {
        isPushedDown = !isPushedDown;
        ChangeAnimationState(BUTTON_PRESS);
        switchCollider.enabled = false;
    }

    
    private void UnpressButton()
    {
        //Release button animation:
        //Disable collider
        if(isPushedDown)
        {
            isPushedDown = false;
            StartCoroutine(ButtonRelease(switchCollider));
        }
        else
        {
            Debug.Log("A coinSwitch is trying to unpress a button not currently pushed down (should only trigger once per press)");
        }
    }

    //Releases the button: reenables collider + plays release animation
    //Takes the box collider as a parameter so I don't have to redeclare
    IEnumerator ButtonRelease(BoxCollider2D switchCollider)
    {
        //Release animation
        ChangeAnimationState(BUTTON_RELEASE);

        //The delay before the button is pressable:
        //yield return new WaitForSeconds(0.8f);

        //Reenable collider so the button can be pressed again
        switchCollider.enabled = true;
        yield return null;
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
