using UnityEngine;

public abstract class EnemyAttackState : EnemyState
{
    protected EnemyAttackState(EnemyController enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }
}
