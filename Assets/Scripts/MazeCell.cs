using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public bool visited = false;
    public bool hasCollectable = false;
    public GameObject northWall;
    public GameObject southWall;
    public GameObject westWall;
    public GameObject eastWall;

}
