using UnityEngine;

public class ZRangeState : TRangeState
{
    protected Zylad zylad => tower as Zylad;
    public ZRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (triggerCalled1)
        {
            triggerCalled1 = false;
            if (tower.nearestREnemy == null) towerFSM.ChangeState(tower.idleState);
        }

        if (triggerCalled3 && tower.nearestREnemy != null && zylad.count == 0) 
        {
            triggerCalled3 = false;
            zylad.Range1();
        }
        else if(triggerCalled3 && tower.nearestREnemy != null && zylad.count == 1)
        {
            triggerCalled3 = false;
            zylad.Range2();
        }
        else if (triggerCalled3 && tower.nearestREnemy != null && zylad.count == 2)
        {
            triggerCalled3 = false;
            zylad.Range3();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
