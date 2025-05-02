using UnityEngine;

public class DMRangeState : TRangeState
{
    protected Darkmur darkmur => tower as Darkmur;
    public DMRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            darkmur.StartShooting(tower.nearestREnemy.transform.position);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
