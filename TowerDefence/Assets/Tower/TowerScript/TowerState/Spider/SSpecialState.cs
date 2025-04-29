using UnityEngine;

public class SSpecialState : TSpecialState
{
    protected Spider spider => tower as Spider;
    public SSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
            //spider.
            triggerCalledStart = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
