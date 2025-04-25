using UnityEngine;

public class TRangeState : TowerState
{
    protected Guardian guardian => tower as Guardian;
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
        if (triggerCalledEnd)
        {
            if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.meleeState);
            else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.moveState);
            else if (tower.nearestREnemy == null) towerFSM.ChangeState(tower.fsmLibrary.idleState);
            triggerCalledEnd = false;
        }

        if (triggerCalled && tower.nearestREnemy != null) 
        { 
            guardian.Shoot(guardian.firePoint.transform.position, tower.nearestREnemy.transform.position); 
            triggerCalled = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
