using UnityEditor;
using UnityEngine;

public class HMoveState : TowerState
{
    public HMoveState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.hMeleeState);
        else if (tower.nearestEnemy == null) towerFSM.ChangeState(tower.fsmLibrary.hRangeState);
        tower.rb.linearVelocity = tower.dir * tower.moveSpeed;
    }
    public override void Exit()
    {
        base.Exit();
    }
}
