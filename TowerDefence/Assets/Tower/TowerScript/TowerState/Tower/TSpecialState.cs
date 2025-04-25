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
        if (triggerSpecial)
        {
            towerFSM.ChangeState(tower.idleState);
            triggerSpecial = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
