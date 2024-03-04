
using UnityEngine;

public class crateState : MonoBehaviour
{
    stateFlip stateFlip;

    // Start is called before the first frame update
    void Start()
    {
        stateFlip = GetComponent<stateFlip>();
        stateFlip.setState(true);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = stateFlip.getState();
    }
}
