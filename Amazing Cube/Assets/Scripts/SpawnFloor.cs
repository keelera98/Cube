using System.Collections.Generic;
using UnityEngine;

public class SpawnFloor : MonoBehaviour
{
    public GameObject floorTile;
    public int floorSize;
    public GameObject[,] Floor;
    private bool validTile = false;

    bool completed = false;

    int counter = 0;

    //Holds the currently generated tiles x and z position
    int currentX, currentZ;
    public int startX = 0;
    public int startZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Fills a 2d array with tiles
        Floor = new GameObject[floorSize, floorSize];

        for (int x = 0; x < floorSize; x++)
        {
            for (int z = 0; z < floorSize; z++)
            {
                Floor[x, z] = Instantiate(floorTile, new Vector3(x, 0, z), Quaternion.identity);
                Floor[x, z].SetActive(false);
            }
        }

        //Checks if numbers are valid for tile selection
        while (!validTile)
        {
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
                    if (randZ == 0 || randZ == floorSize)
                    {
                        Floor[randX, randZ].SetActive(true);
                        startX = randX;
                        startZ = randZ;
                        validTile = true;
                    }
                }
            }
        }

        //Sets current x/z to start x/z
        currentX = startX;
        currentZ = startZ;

        //Keeps generating a path utill we get the one we want
        while (!completed)
        {
            createPath();
        }
    }

    //Create path function
    void createPath()
    {
        //Debug.Log("Xb " + currentX);
        //Debug.Log("Yb " + currentZ);
        //Decides to add/subtract from x/z
        int nextTilePos = Random.Range(0, 4);

        //Checks if tile pos is not greater or less than array size 
        if (nextTilePos == 0 && (currentX - 1) >= 0)
        {
            if (Floor[currentX - 1, currentZ].activeInHierarchy == false) //Checks if tile is not currently active
            {
                Floor[currentX - 1, currentZ].SetActive(true);
                currentX--;
                //Debug.Log("z-");
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        else if (nextTilePos == 1 && (currentX + 1) < floorSize)
        {
            if (Floor[currentX + 1, currentZ].activeInHierarchy == false)
            {
                Floor[currentX + 1, currentZ].SetActive(true);
                currentX++;
                //Debug.Log("X+");
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        else if (nextTilePos == 2 && (currentZ - 1) >= 0)
        {
            if (Floor[currentX, currentZ - 1].activeInHierarchy == false)
            {
                Floor[currentX, currentZ - 1].SetActive(true);
                currentZ--;
                //Debug.Log("z-");
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        else if (nextTilePos == 3 && (currentZ + 1) < floorSize)
        {
            if (Floor[currentX, currentZ + 1].activeInHierarchy == false)
            {
                Floor[currentX, currentZ + 1].SetActive(true);
                currentZ++;
                //Debug.Log("z+");
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        //Checks if ending point has been reached
        if ((currentX == 0 && startX == floorSize - 1) || (currentX == floorSize - 1 && startX == 0) || (currentZ == 0 && startZ == floorSize - 1) || (currentZ == floorSize - 1 && startZ == 0))
        {
            //Stops generating and sets tileInfo isEnd bool to true
            completed = true;
            Floor[currentX, currentZ].GetComponent<TileInfo>().isEnd = true;
        }

        //If tile placment has failed 20 times in a row then reset
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
            Floor[startX, startZ].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
