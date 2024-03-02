using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonBehaviour : MonoBehaviour
{
    private bool isPushedDown;
    private GameObject[] walls;
    private GameObject door;
    private SpriteRenderer buttonSprite;


    // Start is called before the first frame update
    void Start()
    {
        isPushedDown = false;
        buttonSprite= gameObject.GetComponent<SpriteRenderer>();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Box"))
        {
            isPushedDown = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        /*if (collision.transform.CompareTag("Box"))
        {
            isPushedDown = false;
        }
        isPushedDown = false;*/
    }
}
