using UnityEngine;

public class GRangeState : TRangeState
{
    protected Guardian guardian => tower as Guardian;
    public GRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

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
