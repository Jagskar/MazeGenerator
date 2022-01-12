using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecursiveBacktracker : MonoBehaviour
{
    List<List<int>> grid;
    List<List<Cell>> cells;
    int height = 11;
    int width = 11;


    void SetGridBounds(List<List<Vector3>> maze)
    {
        width = maze[0].Count;
        height = maze[1].Count;
    }

    public List<List<Cell>> RegenerateMaze(List<List<Vector3>> oldMaze)
    {
        SetGridBounds(oldMaze);
        CreateGrid(oldMaze);
        GeneratePaths(0, 0, grid);
        FillMaze();
        return cells;
    }

    public void CreateGrid(List<List<Vector3>> maze)
    {
        grid = new List<List<int>>();
        cells = new List<List<Cell>>();
        List<Cell> row = new List<Cell>();
        List<int> visited = new List<int>();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                row.Add(new Cell(maze[i][j]));
                visited.Add(0);
            }
            cells.Add(row);
            grid.Add(visited);
            row = new List<Cell>();
            visited = new List<int>();
        }
    }

    List<Direction> Randomize(List<Direction> dirs)
    {
        List<Direction> toReturn = dirs;
        System.Random random = new System.Random();
        int n = dirs.Count;
        int k;
        while (n > 1)
        {
            k = random.Next(n);
            n--;
            Direction value = toReturn[k];
            toReturn[k] = toReturn[n];
            toReturn[n] = value;
        }
        return toReturn;
    }

    void GeneratePaths(int currX, int currY, List<List<int>> grid)
    {
        List<Direction> dirs = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        dirs = Randomize(dirs);

        foreach(Direction dir in dirs)
        {
            int newX = currX + ConversionFunctions.ToDirectionX(dir);
            int newY = currY + ConversionFunctions.ToDirectionY(dir);

            if ((newY >= 0 && newY <= height - 1) && (newX >= 0 && newX <= width - 1))
            {
                if (grid[newY][newX] == 0) // if not visited
                {
                    grid[currY][currX] |= (int)dir;
                    grid[newY][newX] |= ConversionFunctions.ToOpposite(dir);
                    GeneratePaths(newX, newY, grid);
                }
            }
        }
    }

    void FillMaze()
    {
        ResetMaze();
        string asciiRep = string.Empty;
        string bitwiseRep = string.Empty;

        for (int i = 0; i < height; i++)
        {
            asciiRep += "\n|";
            bitwiseRep += "\n";

            for (int j = 0; j < width; j++)
            {
                asciiRep += (((grid[j][i] & (int)Direction.Down) != 0) ? " " : "_");
                bitwiseRep += "(" + grid[j][i] + ")";
                if ((grid[j][i] & (int)Direction.Down) == 0)
                {
                    cells[j][i].cellBorders.Add(Direction.Down); 
                }
                if ((grid[j][i] & (int)Direction.Right) == 0)
                {
                    cells[j][i].cellBorders.Add(Direction.Right);
                    asciiRep += "|";
                }
            }
        }
    }

    void ResetMaze()
    {
        for (int i = 0; i < height; i++)
        {
            cells[i][0].cellBorders.Add(Direction.Left);
        }

        for (int i = 0; i < width; i++)
        {
            cells[0][i].cellBorders.Add(Direction.Up);
        }

    }
}