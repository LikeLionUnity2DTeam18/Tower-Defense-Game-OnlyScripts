using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public static Vector2 TargetPostion;

    private void Awake()
    {
        TargetPostion = transform.position;
    }
}
