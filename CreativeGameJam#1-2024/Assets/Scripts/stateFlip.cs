using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stateFlip : MonoBehaviour
{
    // Start is called before the first frame update
    bool state;

    public bool getState()
    {
        return state;
    }

    public void setState(bool st)
    {
        state = st;
    }

    public bool Flip(bool st)
    {   
        state = !st;
        return !st;
    }
}
