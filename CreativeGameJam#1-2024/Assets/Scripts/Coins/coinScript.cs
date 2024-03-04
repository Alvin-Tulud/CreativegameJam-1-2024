
using UnityEngine;

public class coinScript : MonoBehaviour
{
    public bool blockEnabled;       //TRUE if coin block, FALSE if coin
    private GameObject coinBlock;   //The block child of every coin. Disabled/enabled by the coin switch

    //When you pick up a coin, you collect it
    //When you hit a P-Switch, coins will turn into coin blocks and vice versa

    // Start is called before the first frame update
    void Start()
    { 
        //assigns the coin's child as coinBlock
        coinBlock = this.gameObject.transform.GetChild(0).gameObject;
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    //Delete the coin if a player touches it
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("im colliding coin " + col.name);
        //Confirm that the player is what is touching the coin:
        if (col.gameObject.CompareTag("Player"))
        {
            //Then delete the coin
            Destroy(this.gameObject);
        }
    }

    //Turns coins to coinblocks and vice versa.
    public void CoinSwitch()
    {
        //If the coin is a block: Turn it back into a coin
        if (blockEnabled)
        {
            coinBlock.SetActive(false);
            //Enable coin collider?
            blockEnabled = false;
        }
        //If the coin is a coin: Turn it into a block
        else
        {
            coinBlock.SetActive(true);
            //Disable coin collider?
            blockEnabled = true;
        }
    }

    /* //Turns coin blocks back into coins.
    public void CoinBlockDisable()
    {

    }

    //Solidifies coins into blocks.
    public void CoinBlockEnable()
    {

    }*/
}
