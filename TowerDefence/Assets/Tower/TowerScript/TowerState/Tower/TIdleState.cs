using UnityEngine;

public class TIdleState : TowerState
{
    public TIdleState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
        if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.meleeState);
        else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.moveState);
        else if (tower.nearestREnemy != null) towerFSM.ChangeState(tower.rangeState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
