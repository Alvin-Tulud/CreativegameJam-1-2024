using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonBehaviour : MonoBehaviour
{
    private bool isPushedDown;
    private SpriteRenderer buttonSprite;

    public GameObject[] walls;
    public GameObject door;
    public GameObject[] coins;


    // Start is called before the first frame update
    void Start()
    {
        isPushedDown = false;
        buttonSprite= gameObject.GetComponent<SpriteRenderer>();

        walls = GameObject.FindGameObjectsWithTag("Wall");
        door = GameObject.FindGameObjectWithTag("Door");
        coins = GameObject.FindGameObjectsWithTag("Coin");
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
        if(collision.transform.CompareTag("Box"))
        {
            isPushedDown = true;
            changeLevelState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Box"))
        {
            isPushedDown = false;
        }
    }

    private void changeLevelState()
    {   
        foreach (GameObject w in walls)
        {
            w.GetComponent<OnOffPlatformState>().changePassthroughState();
        }

    }
}
