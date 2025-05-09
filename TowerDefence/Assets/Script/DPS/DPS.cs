using UnityEngine;

public class DPS : MonoBehaviour
{
    [Header("적 데이터")]
    public EnemyData enemyData;

    [SerializeField] private float totalDamage = 0f;
    public float TotalDamage => totalDamage;

    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sideLength = 5f;

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
        MoveInSquare();
    }

    private void OnDamageReceived(float dmg)
    {
        totalDamage += dmg;
    }

    public void ResetDamage()
    {
        totalDamage = 0f;
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
