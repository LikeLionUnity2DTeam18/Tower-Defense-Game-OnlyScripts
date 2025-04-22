using UnityEngine;

public class Tower : MonoBehaviour
{
    //타워 고정 위치
    public GameObject Beacon { get; set; }

    //타워 스텟
    [SerializeField] public float moveSpeed = 1f; //이동 속도


    //타워 방향
    public bool towerFront { get; private set; } = true;//앞인지 뒤인지
    public bool towerRight { get; private set; } = true;//오른쪽인지 왼쪽인지
    public Vector2 dir;


    //타워 공격 범위
    [SerializeField] private float meleeAttack = 1f;
    [SerializeField] private float rangedAttack = 5f;
    [SerializeField] private float detectRange = 2f; //탐지 범위
    public TowerEnemyTest nearestMEnemy { get; private set; } //근접 적
    public TowerEnemyTest nearestREnemy { get; private set; } //원거리 적
    public TowerEnemyTest nearestEnemy { get; private set; } //가장 가까운 적


    //컴포넌트
    protected SpriteRenderer towerSprite;     //플립용
    public Animator anim {get; private set; }
    public Rigidbody2D rb { get; private set; }


    //인스턴스 생성해야 할 것들
    public FSMLibrary fsmLibrary { get; set; } //FSM 라이브러리
    protected TowerFSM towerFSM;

    public virtual void Awake()
    {
        towerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        towerFSM.currentState.Update();
        ChangeDir();
        if(GetoutArea()) transform.position = Beacon.transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedAttack);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    private void ChangeDir()
    {
        if (GetoutArea())
        {
            UpdateDirection(Beacon.transform.position);
        }
        else
        {
            nearestEnemy = FindNearestEnemyByOverlap(transform.position, detectRange, LayerMask.GetMask("Enemy"));
            if (nearestEnemy != null) dir = (nearestEnemy.transform.position - transform.position).normalized; //적과의 거리 계산

            nearestMEnemy = FindNearestEnemyByOverlap(transform.position, meleeAttack, LayerMask.GetMask("Enemy"));
            if (nearestMEnemy != null)
            {
                UpdateDirection(nearestMEnemy.transform.position);
            }
            else if (nearestMEnemy == null)
            {
                nearestREnemy = FindNearestEnemyByOverlap(transform.position, rangedAttack, LayerMask.GetMask("Enemy"));
                if (nearestREnemy != null)
                {
                    UpdateDirection(nearestREnemy.transform.position);
                }
            }
        }

        //앞, 뒤 애니메이션 변경
        if (towerFront == true)
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



    public bool GetoutArea()
    {   if(Beacon == null) return false;
        if (Vector2.Distance(transform.position, Beacon.transform.position) > Beacon.GetComponent<Beacon>().radius)
        {
            dir = (transform.position - Beacon.transform.position).normalized;
            return true;
        }
        else
        {

            return false;
        }
    }





    public void AnimationTriggerEnd()
    {
        towerFSM.currentState.AnimationEndTrigger();
    }
    public void AnimationTriggerStart()
    {
        towerFSM.currentState.AnimationStartTrigger();
    }

    public void AnimationTrigger()
    {
        towerFSM.currentState.AnimationTrigger();
    }
}
