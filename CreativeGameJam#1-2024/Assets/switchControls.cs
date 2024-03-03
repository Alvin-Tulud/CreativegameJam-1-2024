using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchControls : MonoBehaviour
{
    public GameObject world;
    public GameObject player;
    bool worldControl;
    bool playerControl;


    public GameObject mainCamera;
    Vector3 cameraEndPosition;
    Vector3 cameraStartPosition;
    Vector3 cameraCurrentPosition;
    float cameraMax;
    float cameraMin;
    float cameraCurrentSize;
    float time;
    public float speed;


    public GameObject door;


    private ParticleSystem ps;

    //Animation related
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    Animator animator;
    string currentState;
    const string BUTTON_DOWN = "PButton_Down";
    const string BUTTON_UP = "PButton_Up";
    const string BUTTON_PRESS = "PButton_Press";
    const string BUTTON_RELEASE = "PButton_Release";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        world = GameObject.FindWithTag("World");
        playerControl = true;
        worldControl = false;


        mainCamera = GameObject.FindWithTag("MainCamera");
        cameraStartPosition = mainCamera.transform.position;
        cameraMax = 12f;
        cameraMin = 5f;
        time = 0f;

        door = GameObject.FindWithTag("Door");

        //Defines the animator and sets the button to unpressed position
        animator = gameObject.GetComponent<Animator>();
        ChangeAnimationState(BUTTON_UP);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (worldControl)
        {

            //move camera and change size
            if (mainCamera.GetComponent<Camera>().orthographicSize != cameraMax)
            {
                cameraCurrentSize = Mathf.Lerp(cameraMin, cameraMax, time / speed);

                mainCamera.GetComponent<Camera>().orthographicSize = cameraCurrentSize;


                cameraCurrentPosition.x = Mathf.Lerp(cameraStartPosition.x, cameraEndPosition.x, time / speed);
                cameraCurrentPosition.y = Mathf.Lerp(cameraStartPosition.y, cameraEndPosition.y, time / speed);
                cameraCurrentPosition.z = -10f;

                mainCamera.transform.position = cameraCurrentPosition;


                time++;
            }
            else
            {
                //set final destination and size of camera
                time = 0f;

                mainCamera.GetComponent<Camera>().orthographicSize = cameraMax;

                mainCamera.transform.position = new Vector3(cameraEndPosition.x,cameraEndPosition.y, -10f);


                //set player off and world on
                player.GetComponent<playerMove>().enabled = false;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;


                world.GetComponent<worldMove>().enabled = true;
                world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            }


            door.GetComponent<stateFlip>().setState(true);
        }

        if(playerControl)
        {

            //move camera and change size
            if (mainCamera.GetComponent<Camera>().orthographicSize != cameraMin)
            {
                cameraCurrentSize = Mathf.Lerp(cameraMax, cameraMin, time / speed);

                mainCamera.GetComponent<Camera>().orthographicSize = cameraCurrentSize;


                cameraCurrentPosition.x = Mathf.Lerp(cameraStartPosition.x, cameraEndPosition.x, time / speed);
                cameraCurrentPosition.y = Mathf.Lerp(cameraStartPosition.y, cameraEndPosition.y, time / speed);
                cameraCurrentPosition.z = -10f;

                mainCamera.transform.position = cameraCurrentPosition;


                time++;
            }
            else
            {
                //set final destination and size of camera
                time = 0f;

                mainCamera.GetComponent<Camera>().orthographicSize = cameraMin;

                mainCamera.transform.position = new Vector3(cameraEndPosition.x, cameraEndPosition.y, -10f);


                //set player on and world off
                player.GetComponent<playerMove>().enabled = true;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;


                world.GetComponent<worldMove>().enabled = false;
                world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }


            door.GetComponent<stateFlip>().setState(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerControl = !playerControl;
        worldControl = !worldControl;


        player.GetComponent<playerMove>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;


        world.GetComponent<worldMove>().enabled = false;
        world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        cameraEndPosition = world.transform.position;

        ChangeAnimationState(BUTTON_PRESS);

        //im pretty sure these are inverted but this is a game jam and not a bierman assignment
        StartEndDoor doorScript = door.GetComponent<StartEndDoor>();
        if(playerControl)
        {
            doorScript.ChangeAnimationState("Door_Opening");
        }
        else
        {
            doorScript.ChangeAnimationState("Door_Closing");
        }
    }

    //When the player leaves the button:
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Release button animation:
        //Disable collider
        BoxCollider2D switchCollider = gameObject.GetComponent<BoxCollider2D>();
        switchCollider.enabled = false;

        //Wait a few seconds
        //Enable collider
        StartCoroutine(ButtonRelease(switchCollider));
    }

    //(Copied from coinSwitch.cs)
    //Releases the button: After a few seconds, reenables collider + plays release animation
    //Takes the box collider as a parameter so I don't have to redeclare
    IEnumerator ButtonRelease(BoxCollider2D switchCollider)
    {
        //The delay before the button is released:
        yield return new WaitForSeconds(0.8f);

        //Release animation
        ChangeAnimationState(BUTTON_RELEASE);

        //Reenable collider so the button can be pressed again
        switchCollider.enabled = true;
    }

    //Animation related
    //Code source: https://www.youtube.com/watch?v=53Yx8C5s05c
    private void ChangeAnimationState(string newState)
    {
        if (newState == currentState)
        {
            return;
        }

        animator.Play(newState);

        currentState = newState;
    }
}
