using UnityEngine;

public class GOMeleeState : TMeleeState
{
    protected Golem golem => tower as Golem;
    public GOMeleeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        golem.isReady = true;
    }
    public override void Update()
    {
        if (golem.isReady && tower.nearestMEnemy != null)
        {
            golem.isReady = false;
            if (golem.useLeft)
            {
                golem.LStamp();
            }
            else
            {
                golem.RStamp();
            }

            golem.useLeft = !golem.useLeft;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
