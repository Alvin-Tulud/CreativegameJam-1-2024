
using UnityEngine;

public class generalMusic : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource bgmusic;
    public AudioClip[] states;

    GameObject startendDoor;
    StartEndDoor isEnd;
    bool isPlaying;

    void Start()
    {
        bgmusic = GetComponent<AudioSource>();


        startendDoor = GameObject.FindWithTag("Door");
        isEnd = startendDoor.GetComponent<StartEndDoor>();

        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd.getPlayerTrigger() && !isPlaying)
        {
            bgmusic.Stop();
            bgmusic.clip = states[1];
            bgmusic.volume = 0.3f;
            bgmusic.Play();
            isPlaying = true;
        }
    }
}
