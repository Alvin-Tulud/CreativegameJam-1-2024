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
    float cameraMax;
    float cameraMin;
    float cameraCurrentSize;
    float time;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        world = GameObject.FindWithTag("World");
        playerControl = true;
        worldControl = false;


        mainCamera = GameObject.FindWithTag("MainCamera");
        cameraMax = 12f;
        cameraMin = 5f;
        time = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (worldControl)
        {
            if (mainCamera.GetComponent<Camera>().orthographicSize != cameraMax)
            {
                cameraCurrentSize = Mathf.Lerp(cameraMin, cameraMax, time / speed);

                mainCamera.GetComponent<Camera>().orthographicSize = cameraCurrentSize;

                time++;
            }
            else
            {
                mainCamera.GetComponent<Camera>().orthographicSize = cameraMax;

                time = 0f;

                player.GetComponent<playerMove>().enabled = false;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


                world.GetComponent<worldMove>().enabled = true;
                world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            }
        }

        if(playerControl)
        {
            if (mainCamera.GetComponent<Camera>().orthographicSize != cameraMin)
            {
                cameraCurrentSize = Mathf.Lerp(cameraMax, cameraMin, time / speed);

                mainCamera.GetComponent<Camera>().orthographicSize = cameraCurrentSize;

                time++;
            }
            else
            {
                mainCamera.GetComponent<Camera>().orthographicSize = cameraMin;

                time = 0f;

                player.GetComponent<playerMove>().enabled = true;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;


                world.GetComponent<worldMove>().enabled = false;
                world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerControl = !playerControl;
        worldControl = !worldControl;

        player.GetComponent<playerMove>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


        world.GetComponent<worldMove>().enabled = false;
        world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
