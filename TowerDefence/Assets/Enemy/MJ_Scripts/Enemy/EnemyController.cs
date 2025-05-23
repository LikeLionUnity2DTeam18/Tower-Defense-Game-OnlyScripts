
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData Data { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Vector2 MoveDir { get; private set; }
    public float currentHP { get; private set; }
    public float TargetRange => Data.targetRange;
    //public enum StageChangeEventType { Start, End }

    private EnemyStateMachine stateMachine;

    public Transform effectSpawnPoint;    // 이펙트 생성 위치 (optional)

    public Transform currentTarget;
    public Transform baseTarget { get; private set; } // 기본 목표 (EnemyTarget)

    public StageBalanceData stageBalanceData;

    public bool IsBind { get; private set; } = false;


    public void ApplyStageScaling(int currentStage)
    {
        Data.ResetToBaseValues();

        var stats = stageBalanceData.GetStatsForStage(currentStage);
        if (stats == null) return;

        currentHP = Data.maxHealth * stats.healthMultiplier;
        Data.attackPower *= stats.attackMultiplier;
        Data.moveSpeed *= stats.moveSpeedMultiplier;

        Debug.Log($"[Stage Scaling] Stage: {currentStage}, Name : {Data.enemyName} HP: {currentHP}, Attack: {Data.attackPower}, Speed: {Data.moveSpeed}");
    }


    //public struct StageChangeEvent
    //{
    //    public StageChangeEventType EventType;
    //    public int Stage;
    //    public StageData StageData;
    //    public StageChangeEvent(StageChangeEventType type, int stage)
    //    {
    //        EventType = type;
    //        Stage = stage;
    //        StageData = null;
    //    }
    //    public StageChangeEvent(StageChangeEventType type, int stage, StageData stageData)
    //    {
    //        EventType = type;
    //        Stage = stage;
    //        StageData = stageData;
    //    }
    //}

    public void Initialize(EnemyData data)
    {

        Data = data;
        currentHP = data.maxHealth;

        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        stateMachine = GetComponent<EnemyStateMachine>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (Data.enemyAnim != null)
        {
            Animator.runtimeAnimatorController = Data.enemyAnim;
        }
        //목적지 설정
        baseTarget = EnemyTarget.instance.transform;
        currentTarget = baseTarget;

        // 최초 방향 설정
        MoveDir = ((Vector2)currentTarget.position - (Vector2)transform.position).normalized;

        var initialState = EnemyStateFactory.CreateInitialState(this, stateMachine);

        if (initialState != null)
        {
            stateMachine.Initialize(initialState);
        }
    }

    //타워 감지 범위 내에 있는 가장 가까운 타워를 탐지
    public void DetectNearbyTower()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Data.detectTowerRange);

        
        Transform nearest = null;
        float closestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            // Wall, Tower, BaseTower 감지
            if ((hit.CompareTag("Tower") || hit.CompareTag("BaseTower") ||
                hit.gameObject.layer == LayerMask.NameToLayer("Wall"))
                && hit.gameObject != baseTarget)
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    nearest = hit.transform;
                }
            }
        }
        //감지된 가장 가까운 타워를 currentTarget으로 설정
        if (nearest != null)
        {
            currentTarget = nearest;
            Debug.Log($"[Target Detected] Target: {currentTarget.name}");
        }
        // 감지된 게 없거나, 기존 타워가 파괴 -> baseTower로 이동
        else if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
        {
            // 복귀할 기본 목표는 항상 EnemyTarget
            currentTarget = baseTarget;
        }
    }

    //실시간 이동 방향 업데이트
    public void UpdateMoveDir()
    {
        if (currentTarget != null)
        {
            MoveDir = ((Vector2)currentTarget.position - (Vector2)transform.position).normalized;
        }
    }

    // 범위 시각화
    private void OnDrawGizmosSelected()
    {
        if (Data == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TargetRange); // 공격 범위

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Data.detectTowerRange); // 감지 범위

        // 폭발 범위 – Boomer만
        if (Data.enemyType == EnemyType.Boomer)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, Data.explosionRadius);
        }
    }

    //디버그용 체력 테스트
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log($"[테스트] {Data.enemyName} 체력 999 데미지로 즉사 테스트");
            TakeDamage(999);
        }
    }

    //애니메이션 이벤트로 호출
    public void DestroySelf()
    {
        //Debug.Log("제발");
        Destroy(gameObject);
    }
    //DPS 확인용
    public System.Action<float> OnDamageTaken;
    //데미지 입기
    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        SoundManager.Instance.Play(SoundType.EnemyHit, transform);
        //Debug.Log("현재 체력"+currentHP);
        OnDamageTaken?.Invoke(dmg);
        if (currentHP <= 0f)
        {
            currentHP = 0f; // 혹시 모를 음수 방지
            if (Data.enemyType == EnemyType.Boomer)
            {
                if (!(stateMachine.CurrentState is Boomer_AttackState))
                {
                    stateMachine.ChangeState(new Boomer_DeathState(this, stateMachine));
                }
            }
            else
            {
                stateMachine.ChangeState(new Common_DeathState(this, stateMachine));
            }
        }
    }

    //Collider감지
    private void OnTriggerStay2D(Collider2D other)
    {
        if (currentHP <= 0f) return;

        if ((other.CompareTag("Tower") || other.CompareTag("BaseTower") ||
             other.gameObject.layer == LayerMask.NameToLayer("Wall"))
            && other.gameObject.activeSelf)
        {
            // 타겟 후보만 갱신
            Vector2 targetPoint = other.GetComponent<Collider2D>().ClosestPoint(transform.position);
            float distance = Vector2.Distance(transform.position, targetPoint);

            if (distance <= Data.detectTowerRange)  // 탐지 범위 내일 때만
            {
                currentTarget = other.transform;
            }
        }
    }

    //속박 상태
    public void Bind(float duration)
    {
        if (!IsBind)
            StartCoroutine(BindCoroutine(duration));
    }

    private IEnumerator BindCoroutine(float duration)
    {
        IsBind = true;
        Rigidbody2D.linearVelocity = Vector2.zero;
        PlayIdleAnimation();

        yield return new WaitForSeconds(duration);

        IsBind = false;
    }

    public void PlayIdleAnimation()
    {
        if (MoveDir.y > 0)
        {
            Animator.Play("Idle_back");
            SpriteRenderer.flipX = MoveDir.x < 0;
        }
        else
        {
            Animator.Play("Idle_front");
            SpriteRenderer.flipX = MoveDir.x < 0;
        }
    }

}
