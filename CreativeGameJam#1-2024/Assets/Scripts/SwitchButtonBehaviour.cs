using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchButtonBehaviour : MonoBehaviour
{
    private bool isPushedDown;
    private SpriteRenderer buttonSprite;

    public GameObject[] walls;
    public GameObject door;
    public List<GameObject> switching;

    // Start is called before the first frame update
    void Start()
    {
        isPushedDown = false;
        buttonSprite= gameObject.GetComponent<SpriteRenderer>();

        walls = GameObject.FindGameObjectsWithTag("Wall");
        door = GameObject.FindGameObjectWithTag("Door");


        switching.AddRange(walls);
        switching.Add(door);

    }

    // Update is called once per frame
    void Update()
    {
        if (isPushedDown)
        {
            buttonSprite.color = new Color32(32, 67, 144, 255);
        } 
        else
        {
            buttonSprite.color = new Color32(38, 218, 243, 255);
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
        foreach (GameObject g in switching)
        {
            //flip state of each one
            g.GetComponent<stateFlip>().Flip(g.GetComponent<stateFlip>().getState());
        }

    }
}
