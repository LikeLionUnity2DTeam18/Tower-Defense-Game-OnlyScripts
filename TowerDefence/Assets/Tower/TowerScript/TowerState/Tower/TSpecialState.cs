using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TSpecialState : TowerState
{
    public TSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalled4)
        {
            triggerCalled4 = false;
            towerFSM.ChangeState(tower.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
