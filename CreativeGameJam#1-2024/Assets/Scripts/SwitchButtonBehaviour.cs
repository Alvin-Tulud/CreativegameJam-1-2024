using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonBehaviour : MonoBehaviour
{
    public bool isPushedDown;
    private SpriteRenderer buttonSprite;
    BoxCollider2D switchCollider;

    public List<GameObject> switchableElements;
    public GameObject[] redSwitches;       //Holds the list of other switches to flipflop with this one

    //Animation related (Copied from coinSwitch.cs)
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    Animator animator;
    string currentState;
    const string BUTTON_DOWN = "OFButton_Down";
    const string BUTTON_UP = "OFButton_Up";
    const string BUTTON_PRESS = "OFButton_Press";
    const string BUTTON_RELEASE = "OFButton_Release";


    AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        buttonSprite = gameObject.GetComponent<SpriteRenderer>();
        switchCollider = gameObject.GetComponent<BoxCollider2D>();  //Collider to disable/enable

        //Gather all other PSwitches to flip with this one
        redSwitches = GameObject.FindGameObjectsWithTag("PSwitch");

        // Gather all elements that have the ability to be switched
        setListOfSwitchableElements(new string[] {"Wall"});

        //Defines the animator and sets the button to unpressed position
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

    // Allows for a collection of switchable elements using an array of strings, representing the tags assigned to each of the elements.
    private void setListOfSwitchableElements(string[] tags)
    {
        foreach( string tag in tags)
        {
            switchableElements.AddRange(new List<GameObject> (GameObject.FindGameObjectsWithTag(tag)));
        }
    }



    // Update is called once per frame
    void Update()
    {
  
    }

    //When the button is pressed:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Unpress all pressed buttons, press all unpressed buttons
        foreach (GameObject w in redSwitches)
        {
            SwitchButtonBehaviour s = w.GetComponent<SwitchButtonBehaviour>();
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
        changeLevelState();
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
        if (isPushedDown)
        {
            isPushedDown = false;
            StartCoroutine(ButtonRelease(switchCollider));
        }
        else
        {
            Debug.Log("A redSwitch is trying to unpress a button not currently pushed down (should only trigger once per press)");
        }
    }

    //(Copied from coinSwitch.cs)
    //Releases the button: After a few seconds, reenables collider + plays release animation
    //Takes the box collider as a parameter so I don't have to redeclare
    IEnumerator ButtonRelease(BoxCollider2D switchCollider)
    {
        //The delay before the button is released:
        //yield return new WaitForSeconds(0.8f);

        //Release animation
        ChangeAnimationState(BUTTON_RELEASE);

        //Reenable collider so the button can be pressed again
        switchCollider.enabled = true;

        yield return null;
    }

    private void changeLevelState()
    {   
        foreach (GameObject element in switchableElements)
        {
            //flip state of each one
            element.GetComponent<stateFlip>().Flip();
        }

    }

    //Animation related (Copied from coinSwitch.cs)
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    private void ChangeAnimationState(string newState)
    {
        //if (newState == currentState)
        {
            //return;
        }

        animator.Play(newState);

        currentState = newState;
    }
}
