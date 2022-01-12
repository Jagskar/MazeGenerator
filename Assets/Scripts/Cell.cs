using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<Direction> cellBorders;
    public Vector3 position;

    public Cell(Vector3 pos)
    {
        position = pos;
        cellBorders = new List<Direction>();
    }
}
