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


        /*player.GetComponent<playerMove>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


        world.GetComponent<worldMove>().enabled = false;
        world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        */

        cameraEndPosition = world.transform.position;
    }
}
