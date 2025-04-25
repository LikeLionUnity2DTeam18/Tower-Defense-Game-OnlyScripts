using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController enemy;
    protected EnemyStateMachine stateMachine;

    protected float stateTimer;

    protected EnemyState(EnemyController enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
    }
    public abstract void Exit();
}
