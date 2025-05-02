using UnityEngine;

public class HRangeState : TRangeState
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

        if (triggerCalled3 && tower.nearestREnemy != null) 
        {
            triggerCalled3 = false;
            hyem.StartShooting(tower.nearestREnemy.transform.position); 
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
