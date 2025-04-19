using UnityEngine;

public enum Quadrant
{
    Q1, // ¢Ö
    Q2, // ¢Ø
    Q3, // ¢×
    Q4  // ¢Ù
}


public class TowerDirection : MonoBehaviour
{
    public static Quadrant GetQuadrant(Vector2 origin, Vector2 target)
    {
        Vector2 dir = target - origin;

        if (dir.x >= 0 && dir.y >= 0)
            return Quadrant.Q1; // ¢Ö
        else if (dir.x < 0 && dir.y >= 0)
            return Quadrant.Q2; // ¢Ø
        else if (dir.x < 0 && dir.y < 0)
            return Quadrant.Q3; // ¢×
        else // dir.x >= 0 && dir.y < 0
            return Quadrant.Q4; // ¢Ù
    }


    
}
