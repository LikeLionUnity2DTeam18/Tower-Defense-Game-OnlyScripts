using UnityEngine;

public class Clawer_DeadState : EnemyState
{
    public Clawer_DeadState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

}
