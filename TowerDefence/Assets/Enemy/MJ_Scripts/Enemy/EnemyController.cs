
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData Data { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Vector2 MoveDir { get; private set; }
    public float currentHP { get; private set; }
    public float TargetRange => Data.targetRange;

    private EnemyStateMachine stateMachine;
    public Transform effectSpawnPoint;    // 이펙트 생성 위치 (optional)

    public void Initialize(EnemyData data)
    {

        Data = data;

        //Debug.Log($"[Initialize] 생성된 적 이름: {Data.enemyName}, 타입: {Data.enemyType}");

        currentHP = data.maxHealth;
        Animator = GetComponent<Animator>();
        stateMachine = GetComponent<EnemyStateMachine>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (Data.enemyAnim != null)
        {
            Animator.runtimeAnimatorController = Data.enemyAnim;
        }

        MoveDir = (EnemyTarget.TargetPostion - (Vector2)transform.position).normalized;

        var initialState = EnemyStateFactory.CreateInitialState(this, stateMachine);

        if (initialState != null)
        {
            stateMachine.Initialize(initialState);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0f)
        {
            currentHP = 0f; // 혹시 모를 음수 방지
            //stateMachine.ChangeState(new DeadState(this, stateMachine)); // 나중에 DeadState 연결
        }
    }
}
