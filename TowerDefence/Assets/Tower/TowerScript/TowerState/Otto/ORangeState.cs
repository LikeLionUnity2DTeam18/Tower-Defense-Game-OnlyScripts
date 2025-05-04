using UnityEngine;

public class ORangeState : TRangeState
{
    protected Otto otto => tower as Otto;
    public ORangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            otto.Range();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
