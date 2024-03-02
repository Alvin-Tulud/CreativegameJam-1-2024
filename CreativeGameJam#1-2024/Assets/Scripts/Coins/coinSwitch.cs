using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSwitch : MonoBehaviour
{
    private bool isPushedDown;
    private SpriteRenderer buttonSprite;

    public GameObject[] coins;

    // Start is called before the first frame update
    void Start()
    {
        isPushedDown = false;
        buttonSprite = gameObject.GetComponent<SpriteRenderer>();

        //coins = GameObject.FindGameObjectsWithTag("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        changeLevelState();
        isPushedDown = !isPushedDown;

        if (isPushedDown)
        {
            buttonSprite.color = new Color32(32, 67, 144, 255);
        }
        else
        {
            buttonSprite.color = new Color32(38, 218, 243, 255);
        }
    }

    private void changeLevelState()
    {
        //Need to re-get the list of coins since some may have been picked up
        coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject w in coins)
        {
            //flip state of each one
            w.GetComponent<coinScript>().CoinSwitch();
        }

    }
}
