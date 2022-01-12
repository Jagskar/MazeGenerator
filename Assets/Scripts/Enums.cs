using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum DirectionX
    {
        Right = 1,
        Left = -1,
        Up = 0,
        Down = 0
    }

    public enum DirectionY
    {
        Right = 0,
        Left = 0,
        Up = -1,
        Down = 1
    }

    // Direction enum reversed
    public enum Reverse
    {
        Right = 8,
        Left = 4,
        Down = 2,
        Up = 1
    }
}
