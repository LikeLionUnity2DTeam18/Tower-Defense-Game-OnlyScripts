using UnityEngine;

public class DSpecialS : TowerState
{
    protected DeerGod deerGod => tower as DeerGod;
    public DSpecialS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            towerFSM.ChangeState(tower.fsmLibrary.dIdleS);
        }
    }
}
