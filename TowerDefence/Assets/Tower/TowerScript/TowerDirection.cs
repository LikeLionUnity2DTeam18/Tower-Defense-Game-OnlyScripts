using UnityEngine;
//사분면
public enum Quadrant
{
    Q1, // ↗
    Q2, // ↖
    Q3, // ↙
    Q4  // ↘
}


public class TowerDirection : MonoBehaviour
{
    public static Quadrant GetQuadrant(Vector2 origin, Vector2 target)
    {
        Vector2 dir = target - origin;

        if (dir.x >= 0 && dir.y >= 0)
            return Quadrant.Q1; // ↗
        else if (dir.x < 0 && dir.y >= 0)
            return Quadrant.Q2; // ↖
        else if (dir.x < 0 && dir.y < 0)
            return Quadrant.Q3; // ↙
        else // dir.x >= 0 && dir.y < 0
            return Quadrant.Q4; // ↘
    }


    
}
