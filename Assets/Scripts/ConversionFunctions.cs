using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversionFunctions
{
    public static int ToDirectionX(Direction dir)
    {
        switch(dir)
        {
            case Direction.Up:
                return (int)Enums.DirectionX.Up;
            case Direction.Down:
                return (int)Enums.DirectionX.Down;
            case Direction.Left:
                return (int)Enums.DirectionX.Left;
            case Direction.Right:
                return (int)Enums.DirectionX.Right;
        }
        return 0;
    }

    public static int ToDirectionY(Direction dir)
    {
        switch(dir)
        {
            case Direction.Up:
                return (int)Enums.DirectionY.Up;
            case Direction.Down:
                return (int)Enums.DirectionY.Down;
            case Direction.Left:
                return (int)Enums.DirectionY.Left;
            case Direction.Right:
                return (int)Enums.DirectionY.Right;
        }
        return 0;
    }

    public static int ToOpposite(Direction dir)
    {
        switch(dir)
        {
            case Direction.Up:
                return (int)Enums.Reverse.Up;
            case Direction.Down:
                return (int)Enums.Reverse.Down;
            case Direction.Left:
                return (int)Enums.Reverse.Left;
            case Direction.Right:
                return (int)Enums.Reverse.Right;
        }
        return 0;
    }
}
