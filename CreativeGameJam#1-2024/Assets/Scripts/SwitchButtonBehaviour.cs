using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchButtonBehaviour : MonoBehaviour
{
    private bool isPushedDown;
    private Animator spriteAnimator;

    public List<GameObject> switchableElements;

    // Start is called before the first frame update
    void Start()
    {
        isPushedDown = false;
        spriteAnimator = GetComponent<Animator>();

        // Gather all elements that have the ability to be switched
        setListOfSwitchableElements(new string[] {"Wall","Door"});
        
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
        spriteAnimator.SetBool("isPushed",isPushedDown);
    }
    
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        changeLevelState();
        isPushedDown = !isPushedDown;
    }

    private void changeLevelState()
    {   
        foreach (GameObject element in switchableElements)
        {
            //flip state of each one
            element.GetComponent<stateFlip>().Flip();
        }

    }
}
