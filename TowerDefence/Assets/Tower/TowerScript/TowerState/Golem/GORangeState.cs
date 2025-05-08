using UnityEngine;

public class GORangeState : TRangeState
{
    protected Golem golem => tower as Golem;
    public GORangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        golem.isReady = true;
    }
    public override void Update()
    {
        if (golem.isReady && tower.nearestREnemy != null)
        {
            golem.isReady = false;
            if (golem.useLeft)
            {
                golem.LPunch();
            }
            else
            {
                golem.RPunch();
            }

            golem.useLeft = !golem.useLeft;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
