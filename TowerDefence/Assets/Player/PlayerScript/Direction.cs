using UnityEngine;

public enum Direction4
{
    NW, NE, SW, SE
}

public class Direction
{
    public Direction4 GetDirection(Vector2 dir)
    {
        if (dir.x > 0 && dir.y > 0) return Direction4.NE;
        else if (dir.x > 0 && dir.y < 0) return Direction4.SE;
        else if (dir.x < 0 && dir.y > 0) return Direction4.NW;
        else return Direction4.SW;
    }
}
