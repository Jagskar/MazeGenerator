using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 This class implements the Hunt and kill algorithm for maze generation
 The hunt and kill algorithm works by first being given a grid split into cells 
 Then, the algorithm selects a cell to start with and it enters the kill phase where the walls 
 of the cells are destroyed 
     */
public class Maze : MonoBehaviour
{
    private MazeCell[,] grid;
    private int currRow = 0;
    private int currColumn = 0;
    private bool scanComplete = false;

    public int rows = 1000;
    public int cols = 1000;
    public int numOfCollectables = 3;

    public GameObject wall;
    public GameObject ground;
    public GameObject robot;
    public GameObject collectable;

    public string robotTag = "robot";
    public string collectableTag = "collectable";

    public Vector3 centre;
    public Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 10; i++)
        //{
            GenerateGrid();
            HuntAndKill();
        //}
        SpawnCollectable();
        SpawnRobot();
    }

    int GetNumberOfInstances(string tag)
    {
        GameObject[] instances = GameObject.FindGameObjectsWithTag(tag);
        int count = instances.Length;
        return count;
    }

    void GenerateGrid()
    {
        float wallSize = wall.transform.localScale.x;
        grid = new MazeCell[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                ground = Instantiate(ground, new Vector3(j * wallSize, 0, -i * wallSize), Quaternion.identity) as GameObject; // (0, 0) is at the top left corner 
                ground.name = "Ground: (" + i + ", " + j + ")";

                GameObject northWall = Instantiate(wall, new Vector3(j * wallSize, 1.75f, -i * wallSize + 1.25f), Quaternion.identity);
                northWall.name = "Top Wall: (" + i + ", " + j + ")";

                GameObject southWall = Instantiate(wall, new Vector3(j * wallSize, 1.75f, -i * wallSize - 1.25f), Quaternion.identity);
                southWall.name = "Lower Wall: (" + i + ", " + j + ")";

                GameObject westWall = Instantiate(wall, new Vector3(j * wallSize - 1.25f, 1.75f, -i * wallSize), Quaternion.Euler(0, 90, 0));
                westWall.name = "Left Wall: (" + i + ", " + j + ")";

                GameObject eastWall = Instantiate(wall, new Vector3(j * wallSize + 1.25f, 1.75f, -i * wallSize), Quaternion.Euler(0, 90, 0));
                eastWall.name = "Right Wall: (" + i + ", " + j + ")";

                grid[i, j] = new MazeCell();
                grid[i, j].northWall = northWall;
                grid[i, j].southWall = southWall;
                grid[i, j].westWall = westWall;
                grid[i, j].eastWall = eastWall;

                ground.transform.parent = transform;
                northWall.transform.parent = transform;
                southWall.transform.parent = transform;
                westWall.transform.parent = transform;
                eastWall.transform.parent = transform;


                // Add entrance and exit to maze
                // Disabled to prevent user from falling off edge
                //if (i == 0 && j == 0)
                //    Destroy(westWall);
                //if (i == rows - 1 && j == cols - 1)
                //    Destroy(eastWall);
            }
        }
    }

    bool isNeighborsUnvisited()
    {
        if (isCellUnvisited(currRow - 1, currColumn))
            return true;
        if (isCellUnvisited(currRow + 1, currColumn))
            return true;
        if (isCellUnvisited(currRow, currColumn - 1))
            return true;
        if (isCellUnvisited(currRow, currColumn + 1))
            return true;
        else
            return false;
    }

    bool isCellUnvisited(int row, int col)
    {
        if (row >= 0 && row < rows && col >= 0 && col < cols && !grid[row, col].visited)
            return true;
        else
            return false;
    }

    void HuntAndKill()
    {
        grid[currRow, currColumn].visited = true;
        while (isNeighborsUnvisited())
        {
            int direction = Random.Range(0, 4); // Randomly select a direction: Up, Down, Left, Right
            switch (direction)
            {
                case 0: // Up
                    if (currRow > 0 && !grid[currRow - 1, currColumn].visited)
                    {
                        if (grid[currRow, currColumn].northWall)
                            Destroy(grid[currRow, currColumn].northWall);

                        currRow--;
                        grid[currRow, currColumn].visited = true;

                        if (grid[currRow, currColumn].southWall)
                            Destroy(grid[currRow, currColumn].southWall);
                    }
                    break;
                case 1: // Down
                    if (currRow < rows - 1 && !grid[currRow + 1, currColumn].visited)
                    {
                        if (grid[currRow, currColumn].southWall)
                            Destroy(grid[currRow, currColumn].southWall);

                        currRow++;
                        grid[currRow, currColumn].visited = true;

                        if (grid[currRow, currColumn].northWall)
                            Destroy(grid[currRow, currColumn].northWall);
                    }
                    break;
                case 2: // Left
                    if (currColumn > 0 && !grid[currRow, currColumn - 1].visited)
                    {
                        if (grid[currRow, currColumn].westWall)
                            Destroy(grid[currRow, currColumn].westWall);

                        currColumn--;
                        grid[currRow, currColumn].visited = true;

                        if (grid[currRow, currColumn].eastWall)
                            Destroy(grid[currRow, currColumn].eastWall);
                    }
                    break;
                case 3: // Right
                    if (currColumn < cols - 1 && !grid[currRow, currColumn + 1].visited)
                    {
                        if (grid[currRow, currColumn].eastWall)
                            Destroy(grid[currRow, currColumn].eastWall);

                        currColumn++;
                        grid[currRow, currColumn].visited = true;

                        if (grid[currRow, currColumn].westWall)
                            Destroy(grid[currRow, currColumn].westWall);
                    }
                    break;
            }

            while (!scanComplete)
            {
                Walk();
                Hunt();
            }
        }
    }

    void Walk()
    {
        while (isNeighborsUnvisited())
        {
            int direction = Random.Range(0, 4); // Randomly select a direction: Up, Down, Left, Right
            switch (direction)
            {
                case 0: // Up
                    if (currRow > 0 && !grid[currRow - 1, currColumn].visited)
                    {
                        if (grid[currRow, currColumn].northWall)
                            Destroy(grid[currRow, currColumn].northWall);

                        currRow--;
                        grid[currRow, currColumn].visited = true;

                        if (grid[currRow, currColumn].southWall)
                            Destroy(grid[currRow, currColumn].southWall);
                    }
                    break;
                case 1: // Down
                    if (currRow < rows - 1 && !grid[currRow + 1, currColumn].visited)
                    {
                        if (grid[currRow, currColumn].southWall)
                            Destroy(grid[currRow, currColumn].southWall);

                        currRow++;
                        grid[currRow, currColumn].visited = true;

                        if (grid[currRow, currColumn].northWall)
                            Destroy(grid[currRow, currColumn].northWall);
                    }
                    break;
                case 2: // Left
                    if (currColumn > 0 && !grid[currRow, currColumn - 1].visited)
                    {
                        if (grid[currRow, currColumn].westWall)
                            Destroy(grid[currRow, currColumn].westWall);

                        currColumn--;
                        grid[currRow, currColumn].visited = true;

                        if (grid[currRow, currColumn].eastWall)
                            Destroy(grid[currRow, currColumn].eastWall);
                    }
                    break;
                case 3: // Right
                    if (currColumn < cols - 1 && !grid[currRow, currColumn + 1].visited)
                    {
                        if (grid[currRow, currColumn].eastWall)
                            Destroy(grid[currRow, currColumn].eastWall);

                        currColumn++;
                        grid[currRow, currColumn].visited = true;

                        if (grid[currRow, currColumn].westWall)
                            Destroy(grid[currRow, currColumn].westWall);
                    }
                    break;
            }
        }
    }

    void Hunt()
    {
        scanComplete = true;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (!grid[i, j].visited && isNeighboursVisited(i, j))
                {
                    scanComplete = false;
                    currRow = i;
                    currColumn = j;
                    grid[currRow, currColumn].visited = true;
                    DestroyAdjacentWall();
                    return;
                }
            }
        }
    }

    public bool isNeighboursVisited(int row, int col)
    {
        if (row > 0 && grid[row - 1, col].visited)
            return true;
        if (row < rows - 1 && grid[row + 1, col].visited)
            return true;
        if (col > 0 && grid[row, col - 1].visited)
            return true;
        if (col < cols - 1 && grid[row, col + 1].visited)
            return true;
        else
            return false;
    }

    void DestroyAdjacentWall()
    {
        bool destroyed = false;
        while (!destroyed)
        {
            int direction = Random.Range(0, 4);
            if (direction == 0)
            {
                if (currRow > 0 && grid[currRow - 1, currColumn].visited)
                {
                    if (grid[currRow, currRow].northWall)
                        Destroy(grid[currRow, currColumn].northWall);
                    if (grid[currRow - 1, currColumn].southWall)
                        Destroy(grid[currRow - 1, currColumn].southWall);
                    destroyed = true;
                }
            }
            else if (direction == 1)
            {
                if (currRow < rows - 1 && grid[currRow + 1, currColumn].visited)
                {
                    if (grid[currRow, currColumn].southWall)
                        Destroy(grid[currRow, currColumn].southWall);
                    if (grid[currRow + 1, currColumn].northWall)
                        Destroy(grid[currRow + 1, currColumn].northWall);
                    destroyed = true;
                }
            }
            else if (direction == 2)
            {
                if (currColumn > 0 && grid[currRow, currColumn - 1].visited)
                {
                    if (grid[currRow, currColumn].westWall)
                        Destroy(grid[currRow, currColumn].westWall);
                    if (grid[currRow, currColumn - 1].eastWall)
                        Destroy(grid[currRow, currColumn - 1].eastWall);
                    destroyed = true;
                }
            }
            else
            {
                if (currColumn < cols - 1 && grid[currRow, currColumn + 1].visited)
                {
                    if (grid[currRow, currColumn].eastWall)
                        Destroy(grid[currRow, currColumn].eastWall);
                    if (grid[currRow, currColumn + 1].westWall)
                        Destroy(grid[currRow, currColumn + 1].westWall);
                    destroyed = true; 
                }
            }
        }
    }

    void SpawnRobot()
    {
        // Spawn robot at (0, 0) in the grid
        Vector3 robotPos = new Vector3(0, 0, 0);
        robotPos.x = 1;
        robotPos.y = 1;
        GameObject robotChar;
        if (GetNumberOfInstances(robotTag) == 0)
            robotChar = Instantiate(robot, robotPos, Quaternion.identity) as GameObject;
        else
        {
            robotChar = GameObject.FindWithTag(robotTag);
            robotChar.transform.position = robotPos;
        }
    }

    bool hasCollectable(int x, int y)
    {
        if (grid[x, y].hasCollectable)
            return true;
        return false;
    }

    void SpawnCollectable()
    {
        // Clear scene of all collectables
        if (GetNumberOfInstances(collectableTag) > 0)
            Destroy(GameObject.FindWithTag(collectableTag));

        // Spawn 3 collectables at random positions?
        int x = Random.Range(2, rows);
        int y = Random.Range(2, cols);

        for (int i = 0; i < numOfCollectables; i++)
        {
            // If there is already a collectable, regenerate indices
            while (hasCollectable(x, y))
            {
                x = Random.Range(2, rows);
                y = Random.Range(2, cols);
            }
            Vector3 pos = new Vector3(0, 0, 0);
            Debug.Log("Collectable at: (" + x + ", " + y + ")");
            pos.x = x;
            pos.z = y;
            // place collectable
            GameObject coin = Instantiate(collectable, pos, Quaternion.Euler(0, 0, 90)) as GameObject;
            coin.transform.parent = transform;
            coin.transform.position = pos;
            grid[x, y].hasCollectable = true;
        }
    }

    void SpawnCollectableFixed()
    {
        if (GetNumberOfInstances(collectableTag) > 0)
            Destroy(GameObject.FindWithTag(collectableTag));

        int[] x = { 2, 3, 1 };
        int[] y = { 1, 3, 2 };

        for (int i  = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(0, 0, 0);
            pos.x = x[i];
            pos.y = y[i];

            GameObject coin = Instantiate(collectable, pos, Quaternion.Euler(0, 0, 90)) as GameObject;
            coin.transform.parent = transform;
            coin.transform.position = pos;
            grid[x[i], y[i]].hasCollectable = true;
        }

    }
}
