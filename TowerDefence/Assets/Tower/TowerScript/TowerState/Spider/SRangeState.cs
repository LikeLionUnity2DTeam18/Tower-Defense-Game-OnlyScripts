using UnityEngine;

public class SRangeState : TRangeState
{
    protected Spider spider => tower as Spider;
    public SRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            //spider.
            triggerCalled = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
