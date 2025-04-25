using UnityEngine;

public class DRangeS : TRangeState
{
    protected DeerGod deerGod => tower as DeerGod;
    public DRangeS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled && tower.nearestREnemy != null)
        {
            deerGod.StartProjectile(deerGod.transform.position, tower.nearestREnemy.transform.position);
            triggerCalled = false;
        }
    }
}
