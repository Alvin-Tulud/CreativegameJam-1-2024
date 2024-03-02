using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchControls : MonoBehaviour
{
    public GameObject world;
    public GameObject player;
    bool worldControl;
    bool playerControl;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        world = GameObject.FindWithTag("World");
        playerControl = true;
        worldControl = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (worldControl)
        {
            player.GetComponent<playerMove>().enabled = false;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            world.GetComponent<worldMove>().enabled = true;
            world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }

        if(playerControl)
        {
            player.GetComponent<playerMove>().enabled = true;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            world.GetComponent<worldMove>().enabled = false;
            world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerControl = !playerControl;
        worldControl = !worldControl;
    }
}
