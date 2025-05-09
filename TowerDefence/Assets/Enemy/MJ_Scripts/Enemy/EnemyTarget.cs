using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public static EnemyTarget instance;

    private void Awake()
    {
        instance = this;
    }

    public static Vector2 TargetPostion => instance.transform.position;
}
