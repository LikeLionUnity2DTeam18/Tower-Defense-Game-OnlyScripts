using UnityEngine;

public class TRangeState : TowerState
{
    public TRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalled1)
        {
            triggerCalled1 = false;
            if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.meleeState);
            else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.moveState);
            else if (tower.nearestREnemy == null) towerFSM.ChangeState(tower.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
