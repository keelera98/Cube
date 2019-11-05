using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour
{

    // Create public variables for player speed, and for the Text UI game objects
    public float speed = 0;
    public float speedMod;
    public GameObject floor;
    public float waitTime;
    private float fallTime = 5f;
    //public Text countText;
    public Text winText;
    private bool isColliding = false;

    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    private Rigidbody rb;
    private int count;

    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

        //Moves player to starting position
        transform.position = new Vector3(floor.GetComponent<SpawnFloor>().startX, 0.5f, floor.GetComponent<SpawnFloor>().startZ);
    }

    void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime < 0)
            speed = speedMod;

        //If is in air for more than 5 sec, player has likly fallen
        if (isColliding)
        {
            fallTime -= Time.deltaTime;
            if (fallTime < 2.5f)
            {
                transform.position = new Vector3(floor.GetComponent<SpawnFloor>().startX, 0.5f, floor.GetComponent<SpawnFloor>().startZ);
                rb.constraints = RigidbodyConstraints.FreezeAll;

            }
            if(fallTime < 0)
            {
                rb.constraints = RigidbodyConstraints.None;
                fallTime = 5f;
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

    void OnCollisionEnter(Collision collision)
    {
        //Checks for end tile
        if(collision.gameObject.name == "FloorTile(Clone)")
        {
            if(collision.gameObject.GetComponent<TileInfo>().isEnd == true)
            {
                winText.text = "You Win!";
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isColliding = false;
    }

    void OnCollisionExit(Collision collision)
    {
        isColliding = true;
    }
}