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
        if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.idleState);
        else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.idleState);
        else if (tower.nearestREnemy == null) towerFSM.ChangeState(tower.fsmLibrary.idleState);

        if (triggerCalled && tower.nearestREnemy != null) 
        { 
            guardian.Shoot(tower.transform.position, tower.nearestREnemy.transform.position); 
            triggerCalled = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
