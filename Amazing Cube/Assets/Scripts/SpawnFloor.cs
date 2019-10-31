using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloor : MonoBehaviour
{
    public GameObject floorTile;
    public int floorSize;
    public GameObject[,] Floor;
    private bool validTile = false;

    public int numConFails = 0;

    bool completed = false;

    int counter = 0;

    //Holds starting tile's x and z position for player spawn 
    int startX = 0;
    int startZ = 0;

    //holds end tiles
    int endX = 0;
    int endZ = 0;

    //Holds the currently generated tiles x and z position
    int currentX, currentZ;

    // Start is called before the first frame update
    void Start()
    {
        //Fills a 2d array with tiles
        Floor = new GameObject[floorSize, floorSize];

        for (int x = 0; x < floorSize; x++)
        {
            for (int z = 0; z < floorSize; z++)
            {
                Floor[x,z] = Instantiate(floorTile, new Vector3(x, 0, z), Quaternion.identity);
                Floor[x,z].SetActive(false);
            }
        }

        //Checks if numbers are valid for tile selection
        while (!validTile)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            int randX = Random.Range(0, floorSize);
            int randZ = Random.Range(0, floorSize);

            //Picks a starting tile along the edge of floor plane
            for (int x = 0; x < floorSize; x++)
            {
                for (int z = 0; z < floorSize; z++)
                {
                    //Sets random edge tile to true
                    if (randX == 0 || randX == floorSize)
                    {
                        Floor[randX, randZ].SetActive(true);
                        startX = randX;
                        startZ = randZ;
                        validTile = true;
                    }
                    else if (randZ == 0 || randZ == floorSize)
                    {
                        Floor[randX, randZ].SetActive(true);
                        startX = randX;
                        startZ = randZ;
                        validTile = true;
                    }
                }
            }
        }

        //Checks if numbers are valid for tile selection
        while (!validTile)
        {
            int randX = Random.Range(0, floorSize);
            int randZ = Random.Range(0, floorSize);

            //Picks a ending tile along the edge of floor plane
            for (int x = 0; x < floorSize; x++)
            {
                for (int z = 0; z < floorSize; z++)
                {
                    //Sets random edge tile to true
                    if ((randX == 0 || randX == floorSize) && (randX != startX))
                    {
                        Floor[randX, randZ].SetActive(true);
                        endX = randX;
                        endZ = randZ;
                        validTile = true;
                    }
                    else if ((randZ == 0 || randZ == floorSize) && (randZ != startZ))
                    {
                        Floor[randX, randZ].SetActive(true);
                        endX = randX;
                        endZ = randZ;
                        validTile = true;
                    }
                }
            }
        }

        //To Do: Create path to generated endpoint tile, by checking if they are
        //connected and contineing until we reach the endpoint
        currentX = startX;
        currentZ = startZ;
    }

    void createPath(int currentX, int currentZ, int startX, int startZ, int endX, int endZ)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        //Decides to add/subtract from x/z
        int nextTilePos = Random.Range(0, 4);

        //Checks if tile pos is not greater or less than array size 
        if (nextTilePos == 0 && (currentX - 1) >= 0)
        {
            if (Floor[currentX - 1, currentZ].activeInHierarchy == false) //Checks if tile is not currently active
            {
                Floor[currentX - 1, currentZ].SetActive(true);
                currentX--;
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        if (nextTilePos == 1 && (currentX + 1) < floorSize)
        {
            if (Floor[currentX + 1, currentZ].activeInHierarchy == false)
            {
                Floor[currentX + 1, currentZ].SetActive(true);
                currentX++;
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        if (nextTilePos == 2 && (currentZ - 1) >= 0)
        {
            if (Floor[currentX, currentZ - 1].activeInHierarchy == false)
            {
                Floor[currentX, currentZ - 1].SetActive(true);
                currentZ--;
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        if (nextTilePos == 3 && (currentZ + 1) < floorSize)
        {
            if (Floor[currentX, currentZ + 1].activeInHierarchy == false)
            {
                Floor[currentX, currentZ + 1].SetActive(true);
                currentZ++;
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        //Checks if ending point has been reached
        if ((currentX == endX) && (currentZ == endZ))
        {
            completed = true;
            Debug.Log("Reached true");
            Debug.Log(currentZ);
            Debug.Log(currentX);
            Debug.Log(endZ);
            Debug.Log(endX);
        }

        Debug.Log("X "+currentX);
        Debug.Log("Y "+currentZ);
        Debug.Log("R "+nextTilePos);

        if (counter > 20)
        {
            for (int x = 0; x < floorSize; x++)
            {
                for (int z = 0; z < floorSize; z++)
                {
                    Floor[x, z].SetActive(false);
                }
            }
            currentX = startX;
            currentZ = startZ;
            return;
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            createPath(currentX, currentZ, startX, startZ, endX, endZ);
        }
    }
}
