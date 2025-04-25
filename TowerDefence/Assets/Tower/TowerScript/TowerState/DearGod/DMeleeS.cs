using UnityEngine;

public class DMeleeS : TowerState
{
    public DMeleeS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy == null && triggerCalledEnd) towerFSM.ChangeState(tower.fsmLibrary.dMoveS);
    }
}
