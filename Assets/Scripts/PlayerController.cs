using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // Create public variables for player speed, and for the Text UI game objects
    public float speed = 0;
    public float speedMod;
    public GameObject floor;
    public float waitTime;
    private float fallTime = 7.5f;
    private float winTimer = 2.5f;
    public Text winText;
    public Text timeText;
    private bool gameOver, isColliding;

    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    private Rigidbody rb;
    private int count;

    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

        transform.position = new Vector3(floor.GetComponent<SpawnFloor>().startX, 0.5f, floor.GetComponent<SpawnFloor>().startZ);
    }

    void Update()
    {
        waitTime += Time.deltaTime;
        if (waitTime > 0)
        {
            speed = speedMod;
            if(!gameOver)
                timeText.text = "Time: " + (int)waitTime + " seconds";
        }

        //Restarts scene when player reaches finish
        if (gameOver)
        {
            winText.text = "You Win!";

            winTimer -= Time.deltaTime;
            if(winTimer <= 0)
            {
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }

        //Starts timer when isColliding is false 
        if(!isColliding)
        {
            fallTime -= Time.deltaTime;
            if (fallTime < 5f && fallTime > 2.5f)
            {
                //Display you lose text
                winText.text = "You Lose!";
            }
            else if(fallTime < 2.5f && fallTime > 0)
            {
                //Resets text to nothing
                winText.text = " ";

                //Moves player to start and freezes player
                transform.position = new Vector3(floor.GetComponent<SpawnFloor>().startX, 0.5f, floor.GetComponent<SpawnFloor>().startZ);
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            else if(fallTime <= 0)
            {
                //unfreeze player, reset fallTime, and waitTime 
                rb.constraints = RigidbodyConstraints.None;
                waitTime = 0f;
                fallTime = 7.5f;
            }
        }
    }

    // Each physics step..
    void FixedUpdate()
    {
        // Set some local float variables equal to the value of our Horizontal and Vertical Inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
        // multiplying it by 'speed' - our public player speed that appears in the inspector
        rb.AddForce(movement * speed);
    }

    //Checks for end tile
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "FloorTile(Clone)")
        {
            if(collision.gameObject.GetComponent<TileInfo>().isEnd == true)
            {
                gameOver = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }
    void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}