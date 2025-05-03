using UnityEngine;

public class OSpecialState : TSpecialState
{
    protected Otto otto => tower as Otto;
    public OSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            triggerCalled2 = false;
            otto.Special();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
