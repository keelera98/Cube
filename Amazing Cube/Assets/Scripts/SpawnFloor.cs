using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloor : MonoBehaviour
{
    public GameObject floorTile;
    public int floorSize;
    public GameObject[,] Floor;

    // Start is called before the first frame update
    void Start()
    {
        Floor = new GameObject[floorSize, floorSize];

        for (int x = 0; x < floorSize; x++)
        {
            for (int z = 0; z < floorSize; z++)
            {
                Floor[x,z] = Instantiate(floorTile, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
