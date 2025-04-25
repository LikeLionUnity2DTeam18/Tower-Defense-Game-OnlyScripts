using UnityEngine;

public class HIdleState : TowerState
{
    public HIdleState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        tower.rb.linearVelocity = Vector2.zero;
    }
    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.hMeleeState);
        else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.hMoveState);
        else if (tower.nearestREnemy != null) towerFSM.ChangeState(tower.fsmLibrary.hRangeState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
