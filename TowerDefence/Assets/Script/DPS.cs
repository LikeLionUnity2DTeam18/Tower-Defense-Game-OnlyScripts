using UnityEngine;

public class DPS : MonoBehaviour
{
    [Header("적 데이터")]
    public EnemyData enemyData;

    [Header("DPS 측정")]
    [SerializeField] private float damagePerMinute;
    [SerializeField] private float measureDuration = 60f;
    [SerializeField] private float totalDamage = 0f;
    [SerializeField] private float elapsedTime = 0f;

    [Header("이동")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float sideLength = 3f;
    private Vector3 startPos;
    private float movedDistance = 0f;
    private int moveDirIndex = 0;

    private EnemyController enemyController;

    private static readonly Vector3[] directions = new Vector3[]
    {
        Vector3.right, Vector3.up, Vector3.left, Vector3.down
    };

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        enemyController.Initialize(enemyData);
        enemyController.OnDamageTaken += OnDamageReceived;

        startPos = transform.position;
    }

    void Update()
    {
        MeasureDPS();
        MoveInSquare();
    }

    private void OnDamageReceived(float dmg)
    {
        totalDamage += dmg;
    }

    private void MeasureDPS()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= measureDuration)
        {
            damagePerMinute = totalDamage / elapsedTime;
            Debug.Log($"[DPS 측정] 분당 데미지: {damagePerMinute:F2}");

            totalDamage = 0f;
            elapsedTime = 0f;
        }
    }

    private void MoveInSquare()
    {
        float moveStep = moveSpeed * Time.deltaTime;
        transform.Translate(directions[moveDirIndex] * moveStep);
        movedDistance += moveStep;

        if (movedDistance >= sideLength)
        {
            movedDistance = 0f;
            moveDirIndex = (moveDirIndex + 1) % directions.Length;
        }
    }
}
