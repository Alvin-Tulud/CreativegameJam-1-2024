
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

    public void Flip()
    {   
        state = !state;
    }
}
