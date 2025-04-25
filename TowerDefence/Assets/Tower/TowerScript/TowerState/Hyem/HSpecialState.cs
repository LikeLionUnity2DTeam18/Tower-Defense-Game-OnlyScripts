using UnityEngine;

public class HSpecialState : TowerState
{
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
        if (triggerCalled)
        {
            towerFSM.ChangeState(tower.fsmLibrary.hIdleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
