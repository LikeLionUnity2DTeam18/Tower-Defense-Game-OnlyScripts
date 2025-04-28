using UnityEngine;
//애니메이션에서 사용할 레이어
enum layer
{
    Front,
    Back,
}
public interface IStatReceiver  
{
    void SetStats(Tower tower,TowerStats stats);
}
public class Tower : MonoBehaviour
{
    //타워 스텟
    public TowerStats stats {get; private set; }

    //타이머
    [SerializeField] public float timer;


    //비콘 관련 설정
    public GameObject Beacon { get; set; }
    public Beacon beacon { get; set; }

    //타워 방향
    public bool towerFront { get; private set; } = true;//앞인지 뒤인지
    public bool towerRight { get; private set; } = true;//오른쪽인지 왼쪽인지
    public Vector2 dir;


    public GameObject nearestMEnemy { get; private set; } //근접 적
    public GameObject nearestREnemy { get; private set; } //원거리 적
    public GameObject nearestEnemy { get; private set; } //가장 가까운 적

    //스테이트
    public TowerState idleState { get; set; }
    public TowerState moveState { get; set; }
    public TowerState meleeState { get; set; }
    public TowerState rangeState { get; set; }
    public TowerState specialState { get; set; }

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
        stats = GetComponent<TowerStats>();
        towerFSM = new TowerFSM();
    }
    public virtual void Start()
    {
        towerFSM.Init(idleState);
    }

    public virtual void Update()
    {
        towerFSM.currentState.Update();

        if(timer > 0)timer -= Time.deltaTime;
        if (timer <= 0f && (nearestREnemy != null || nearestMEnemy != null || nearestEnemy != null))
        {
            timer = stats.cooldown.GetValue();
            towerFSM.ChangeState(specialState);
        }

        ChangeDir();
        if(GetoutArea() && nearestMEnemy == null) transform.position = Beacon.transform.position;
    }

    void OnDrawGizmosSelected()
    {
        if (stats != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stats.meleeDistance.GetValue());
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, stats.rangeDistance.GetValue());
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stats.moveDistance.GetValue());
        }
    }

    //거리에 따른 탐지 
    private void ChangeDir()
    {
        nearestMEnemy = FindNearestEnemyByOverlap(transform.position, stats.meleeDistance.GetValue(), LayerMask.GetMask("Enemy"));
        if (nearestMEnemy != null)
        {
            UpdateDirection(nearestMEnemy.transform.position);
            dir = (nearestMEnemy.transform.position - transform.position).normalized;
        }
        else if (nearestMEnemy == null)
        {
            nearestEnemy = FindNearestEnemyByOverlap(transform.position, stats.moveDistance.GetValue(), LayerMask.GetMask("Enemy"));
            if (nearestEnemy != null) 
            {
                UpdateDirection(nearestEnemy.transform.position);
                dir = (nearestEnemy.transform.position - transform.position).normalized;
            }
            else if(nearestEnemy == null)
            {
                nearestREnemy = FindNearestEnemyByOverlap(transform.position, stats.rangeDistance.GetValue(), LayerMask.GetMask("Enemy"));
                if (nearestREnemy != null)
                {
                    UpdateDirection(nearestREnemy.transform.position);
                    dir = (nearestREnemy.transform.position - transform.position).normalized;
                }
            }  
        }
        

        //앞, 뒤 애니메이션 변경
        if (towerFront == true)
        {
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
        }
        else if (towerFront == false)
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 1);
        }
        //좌, 우 변경
        Flip();
    }


    //가까운 적 탐지
    public GameObject FindNearestEnemyByOverlap(Vector3 origin, float radius, LayerMask enemyLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyLayer);
        if (hits.Length == 0) return null;

        GameObject nearest = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            float dist = (hit.transform.position - origin).sqrMagnitude;

            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.gameObject;
            }
        }
        return nearest;
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


    //비콘 범위 밖으로 나갈시 비콘 위치로 이동
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

    //타워 움직임 관련
    public void TowerMovement()
    {
        rb.linearVelocity = dir * stats.speed.GetValue();
    }
    public void TowerStop()
    {
        rb.linearVelocity = Vector2.zero;
    }
    
    //스탯 전달
    public GameObject SpawnWithStats(GameObject prefab)
    {
        GameObject obj = PoolManager.Instance.Get(prefab);

        if (obj.TryGetComponent<IStatReceiver>(out var receiver))
        {
            receiver.SetStats(this,this.stats);
        }
        return obj;
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
    public void AnimationTriggerSpeical()
    {
        towerFSM.currentState.AnimationSpecialTrigger();
    }
}
