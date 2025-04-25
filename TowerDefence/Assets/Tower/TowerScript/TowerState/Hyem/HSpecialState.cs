using UnityEngine;

public class HSpecialState : TowerState
{
    protected Hyem hyem => tower as Hyem;
    public HSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalledStart)
        {
            hyem.CastIceCone();
            triggerCalledStart = false;
        }
        if (triggerCalled)
        {
            towerFSM.ChangeState(tower.fsmLibrary.hIdleState);
            triggerCalled = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
