using UnityEngine;

public class Bloody_DeathState : EnemyState
{
    public Bloody_DeathState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
