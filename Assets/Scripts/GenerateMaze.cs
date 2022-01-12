using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateMaze : MonoBehaviour
{
    List<List<Vector3>> grid;
    List<List<Cell>> cells;
    ObjectPoints points;
    int width = 11;
    int height = 11;
    readonly int planeWidth = 11;
    readonly int planeHeight = 11;
    GameObject borderParent;
    RecursiveBacktracker recursiveBacktracker;

    public GenerateMaze()
    {
        // Initialise variables
        grid = new List<List<Vector3>>();
        cells = new List<List<Cell>>();
        
    }

    public void CreateMaze()
    {
        Destroy(borderParent);
        borderParent = new GameObject("BorderParent");

        width = UnityEngine.Random.Range(2, planeWidth + 1);
        height = UnityEngine.Random.Range(2, planeHeight + 1);
        recursiveBacktracker.CreateGrid(grid);
        cells = recursiveBacktracker.RegenerateMaze(grid);
    }

    void CreateGrid()
    {
        grid = new List<List<Vector3>>();
        List<Vector3> row = new List<Vector3>();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                row.Add(points.GetObjectGlobalVertices()[i + j * planeHeight]);
            }
            grid.Add(row);
            row = new List<Vector3>();
        }
    }

    void ShowMaze()
    {
        GameObject verticalBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        verticalBorder.transform.localScale = new Vector3(1.5f, 1, 0.5f);

        GameObject horizontalBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        horizontalBorder.transform.localScale = new Vector3(0.5f, 1, 1.5f);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (cells[j][i].cellBorders.Contains(Direction.Up))
                {
                    InsertHorizontalBorder(horizontalBorder, cells[j][i], Direction.Up);
                }
                if (cells[j][i].cellBorders.Contains(Direction.Down))
                {
                    InsertHorizontalBorder(horizontalBorder, cells[j][i], Direction.Down);
                }
                if (cells[j][i].cellBorders.Contains(Direction.Left))
                {
                    InsertVerticalBorder(verticalBorder, cells[j][i], Direction.Left);
                }
                if (cells[j][i].cellBorders.Contains(Direction.Right))
                {
                    InsertVerticalBorder(verticalBorder, cells[j][i], Direction.Right);
                }
            }
        }
    }

    void InsertHorizontalBorder(GameObject border, Cell cell, Direction dir)
    {
        Instantiate(border, cell.position + Vector3.right * 0.5f * ((dir == Direction.Up) ? 1 : -1), Quaternion.identity, borderParent.transform);
    }

    void InsertVerticalBorder(GameObject border, Cell cell, Direction dir)
    {
        Instantiate(border, cell.position + Vector3.forward * 0.5f * ((dir == Direction.Left) ? 1 : -1), Quaternion.identity, borderParent.transform);
    }

}

