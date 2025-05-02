using UnityEngine;

public class DMSpecialState : TSpecialState
{
    protected Darkmur darkmur => tower as Darkmur;
    public DMSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            if (!darkmur.isClone) darkmur.Clone();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
