using UnityEngine;

public class HSpecialState : TSpecialState
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
        if (triggerCalled2)
        {
            hyem.CastIceCone();
            triggerCalled2 = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
