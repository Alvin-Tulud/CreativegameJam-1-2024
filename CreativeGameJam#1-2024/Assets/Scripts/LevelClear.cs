using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClear : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject world;
    public GameObject player;
    public GameObject mainCamera;
    StartEndDoor door;
    Vector3 cameraEndPosition;
    Vector3 cameraStartPosition;
    Vector3 cameraCurrentPosition;
    float cameraMax;
    float cameraMin;
    float cameraCurrentSize;
    float time;
    public float speed;
    bool doneZooming;
    bool gotPosition;

    void Start()
    {
        world = GameObject.FindWithTag("World");
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
        door = GetComponent<StartEndDoor>();
        cameraMax = mainCamera.GetComponent<Camera>().orthographicSize;
        cameraMin = 1f;
        time = 0f;
        doneZooming = false;
        gotPosition = false;
    }

    // Update is called once per frame

    //on clear zooms in on player 
    void FixedUpdate()
    {
        if(!gotPosition && door.getPlayerTrigger())
        {
            cameraEndPosition = player.transform.position;
            cameraStartPosition = mainCamera.transform.position;

            gotPosition = true;
        }


        if (door.getPlayerTrigger() && !doneZooming)
        {
            world.GetComponent<worldMove>().enabled = false;
            world.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


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
                time = 0f;

                mainCamera.GetComponent<Camera>().orthographicSize = cameraMin;

                mainCamera.transform.position = new Vector3(cameraEndPosition.x, cameraEndPosition.y, -10f);

                doneZooming = true;
            }
        }



        if (cameraCurrentSize == cameraMin)
        {

            if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
