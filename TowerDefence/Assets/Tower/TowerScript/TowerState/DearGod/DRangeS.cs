using UnityEngine;

public class DRangeS : TowerState
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
        if (triggerCalledEnd)
        {
            if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.dIdleS);
            else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.dMoveS);
            else if (tower.nearestREnemy == null) towerFSM.ChangeState(tower.fsmLibrary.dIdleS);
            triggerCalledEnd = false;
        }

        if (triggerCalled && tower.nearestREnemy != null)
        {
            deerGod.StartProjectile(deerGod.transform.position, tower.nearestREnemy.transform.position);
            triggerCalled = false;
        }
    }
}
