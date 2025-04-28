using UnityEngine;

public class WRangeState : TRangeState
{
    protected WatchDog watchDog => tower as WatchDog;
    public WRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            watchDog.ShieldBash();
            triggerCalled = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
