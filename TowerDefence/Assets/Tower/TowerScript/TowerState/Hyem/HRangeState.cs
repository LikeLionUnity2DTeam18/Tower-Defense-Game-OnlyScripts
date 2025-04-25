using UnityEngine;

public class HRangeState : TowerState
{
    protected Hyem hyem => tower as Hyem;
    public HRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalledEnd)
        {
            if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.hMeleeState);
            else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.hMoveState);
            else if (tower.nearestREnemy == null) towerFSM.ChangeState(tower.fsmLibrary.hIdleState);
            triggerCalledEnd = false;
        }

        if (triggerCalled && tower.nearestREnemy != null) 
        {
            hyem.StartShooting(tower.nearestREnemy.transform.position); 
            triggerCalled = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
