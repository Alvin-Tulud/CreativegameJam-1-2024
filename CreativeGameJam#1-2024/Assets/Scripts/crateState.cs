using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateState : MonoBehaviour
{
    stateFlip stateFlip;
    public bool state;

    // Start is called before the first frame update
    void Start()
    {
        stateFlip = GetComponent<stateFlip>();
        stateFlip.setState(state);
    }

    // Update is called once per frame
    void Update()
    {
        if (stateFlip.getState())
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
