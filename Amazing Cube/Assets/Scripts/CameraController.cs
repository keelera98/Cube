using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    // store a public reference to the Player game object, so we can refer to it's Transform
    public GameObject player, floor;

    // Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
    private Vector3 offset;

    public float waitTime;

    // At the start of the game..
    void Start()
    {
        waitTime -= Time.deltaTime;
        if (waitTime > 0)
        {
            // Create an offset by subtracting the Camera's position from the player's position
            transform.position = new Vector3(floor.GetComponent<SpawnFloor>().startX, 5, floor.GetComponent<SpawnFloor>().startZ);
        }
        offset = transform.position - player.transform.position;


    }

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
    void Update()
    {
        // Set the position of the Camera (the game object this script is attached to)
        // to the player's position, plus the offset amount
        transform.position = player.transform.position + offset;
    }
}