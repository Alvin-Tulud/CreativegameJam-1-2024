using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchButtonBehaviour : MonoBehaviour
{
    private bool isPushedDown;
    private SpriteRenderer buttonSprite;

    public List<GameObject> switchableElements;

    // Start is called before the first frame update
    void Start()
    {
        isPushedDown = false;
        buttonSprite= gameObject.GetComponent<SpriteRenderer>();

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
        if (isPushedDown)
        {
            // Darker Blue color represents pressed down
            //buttonSprite.color = new Color32(32, 67, 144, 255);
            GetComponent<Animator>().Play("button_push", -1, 0f);
        } 
        else
        {
            // Lighter Blue color represents unpressed
            //buttonSprite.color = new Color32(38, 218, 243, 255);
        }
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
