using UnityEngine;

public class Tower : MonoBehaviour
{
    private bool towerFront;
    private bool towerRight;
    [SerializeField] private float meleeAttack = 2f;
    [SerializeField] private float rangedAttack = 5f;

    protected SpriteRenderer towerSprite;     //플립용
    protected FSMLibrary fsmLibrary;
    protected TowerFSM towerFSM;
    public Animator anim {get; private set; }

    public virtual void Awake()
    {
        towerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        towerFSM.currentState.Update();
        var nearestEnemy = FindNearestEnemyByOverlap(transform.position, meleeAttack, LayerMask.GetMask("Enemy"));
        if (nearestEnemy != null)
        {
            UpdateDirection(nearestEnemy.transform.position);
        }
        Flip();

        if(towerFront)
        {
            towerFSM.ChangeState(fsmLibrary.tFrontS);
        }
        else if (!towerFront)
        {
            towerFSM.ChangeState(fsmLibrary.tBackS);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedAttack);
    }


    //가까운 적 탐지
    public TowerEnemyTest FindNearestEnemyByOverlap(Vector3 origin, float radius, LayerMask enemyLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyLayer);
        if(hits.Length == 0) return null;
        else
        {
            TowerEnemyTest nearest = null;
            float minDist = float.MaxValue;

            foreach (var hit in hits)
            {
                TowerEnemyTest enemy = hit.GetComponent<TowerEnemyTest>();
                if (enemy == null) continue;

                float dist = (enemy.transform.position - origin).sqrMagnitude;

                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = enemy;
                }
            }
            return nearest;
        }
    }


    //방향 설정
    void UpdateDirection(Vector2 enemyPos)
    {
        if (enemyPos == null)
        {
            return;
        }
        else if (enemyPos != null)
        {
            var quadrant = TowerDirection.GetQuadrant(transform.position, enemyPos); switch (quadrant)
            {
                case Quadrant.Q1:
                    towerFront = false;
                    towerRight = true;
                    break;
                case Quadrant.Q2:
                    towerFront = false;
                    towerRight = false;
                    break;
                case Quadrant.Q3:
                    towerFront = true;
                    towerRight = false;
                    break;
                case Quadrant.Q4:
                    towerFront = true;
                    towerRight = true;
                    break;
            }
        }
    }

    //플립
    void Flip()
    {
        if (towerRight)
        {
            towerSprite.flipX = false;
        }
        else if (!towerRight)
        {
            towerSprite.flipX = true;
        }
    }
}
