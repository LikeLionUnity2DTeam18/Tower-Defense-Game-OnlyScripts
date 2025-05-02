using UnityEngine;

public class GSpecialState : TSpecialState
{
    protected Guardian guardian => tower as Guardian;
    public GSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            guardian.Restraint();
            triggerCalled2 = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
