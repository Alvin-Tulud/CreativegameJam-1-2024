using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class SwitchButtonBehaviour : MonoBehaviour
{
    private bool isPushedDown;
    private SpriteRenderer buttonSprite;

    public List<GameObject> switchableElements;

    //Animation related (Copied from coinSwitch.cs)
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    Animator animator;
    string currentState;
    const string BUTTON_DOWN = "OFButton_Down";
    const string BUTTON_UP = "OFButton_Up";
    const string BUTTON_PRESS = "OFButton_Press";
    const string BUTTON_RELEASE = "OFButton_Release";

    // Start is called before the first frame update
    void Start()
    {
        isPushedDown = false;
        buttonSprite = gameObject.GetComponent<SpriteRenderer>();
        //spriteAnimator = GetComponent<Animator>();
        //spriteAnimator.SetBool("isPushed", isPushedDown);

        // Gather all elements that have the ability to be switched
        setListOfSwitchableElements(new string[] {"Wall"});

        //Defines the animator and sets the button to unpressed position
        animator = gameObject.GetComponent<Animator>();
        ChangeAnimationState(BUTTON_UP);

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
        changeLevelState();
        isPushedDown = !isPushedDown;
        ChangeAnimationState(BUTTON_PRESS);
        
    }

    //(Copied from coinSwitch.cs)
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

    //(Copied from coinSwitch.cs)
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
