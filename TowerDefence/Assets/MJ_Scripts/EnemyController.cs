
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData Data { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Vector2 MoveDir { get; private set; }
    public float TargetRange => targetRange;

    private EnemyStateMachine stateMachine;

    //머가 문제라는거지

    [SerializeField] private float targetRange = 0.5f;

    public void Initialize(EnemyData data)
    {
        Data = data;
        Animator = GetComponent<Animator>();
        stateMachine = GetComponent<EnemyStateMachine>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (Data.enemyAnim != null)
        {
            Animator.runtimeAnimatorController = Data.enemyAnim;
        }

        MoveDir = (EnemyTarget.TargetPostion - (Vector2)transform.position).normalized;

        stateMachine.Initialize(new Blocker_MoveState(this, stateMachine));
    }

    public void TakeDamage(float dmg)
    {
        // 체력 감소 처리 후 죽음 상태로 전환
        //stateMachine.ChangeState(new DeadState(this, stateMachine));
    }
}
