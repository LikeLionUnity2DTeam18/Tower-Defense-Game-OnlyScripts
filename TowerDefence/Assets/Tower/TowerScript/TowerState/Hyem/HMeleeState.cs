using UnityEngine;

public class HMeleeState : TowerState
{
    public HMeleeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy == null && triggerCalledEnd) 
        { 
            towerFSM.ChangeState(tower.fsmLibrary.hMoveState); 
            triggerCalledEnd = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
