using UnityEngine;

public class Tower : MonoBehaviour
{
    //타워 방향
    public bool towerFront { get; private set; } = true;//앞인지 뒤인지
    public bool towerRight { get; private set; } = true;//오른쪽인지 왼쪽인지

    //타워 공격 범위
    [SerializeField] private float meleeAttack = 2f;
    [SerializeField] private float rangedAttack = 5f;


    //컴포넌트
    protected SpriteRenderer towerSprite;     //플립용
    public FSMLibrary fsmLibrary { get; set; } //FSM 라이브러리
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
        ChangeDir();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedAttack);
    }

    private void ChangeDir()
    {
        var nearestMEnemy = FindNearestEnemyByOverlap(transform.position, meleeAttack, LayerMask.GetMask("Enemy"));
        if (nearestMEnemy != null)
        {
            UpdateDirection(nearestMEnemy.transform.position);
        }
        else if (nearestMEnemy == null)
        {
            var nearestREnemy = FindNearestEnemyByOverlap(transform.position, rangedAttack, LayerMask.GetMask("Enemy"));
            if (nearestREnemy != null)
            {
                UpdateDirection(nearestREnemy.transform.position);
            }
        }
        //앞, 뒤 애니메이션 변경
        if(towerFront == true)
        {
            anim.SetBool("Front", true);
        }
        else if (towerFront == false)
        {
            anim.SetBool("Front", false);
        }
        //좌, 우 변경
        Flip();
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


    public void AnimationTrigger()
    {
        towerFSM.currentState.AnimationFinishTrigger();
    }
}
