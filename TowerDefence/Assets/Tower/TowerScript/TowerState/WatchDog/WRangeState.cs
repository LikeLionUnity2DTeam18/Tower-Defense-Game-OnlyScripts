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

        if (triggerCalled3 && tower.nearestREnemy != null) 
        {
            triggerCalled3 = false;
            watchDog.ShieldBash();
        }
        if (triggerCalled5)
        {
            triggerCalled5 = false;
            tower.DoRangeDamage();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
