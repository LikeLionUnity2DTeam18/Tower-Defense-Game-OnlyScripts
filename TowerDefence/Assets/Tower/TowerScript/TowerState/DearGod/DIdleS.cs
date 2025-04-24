using UnityEngine;

public class DIdleS : TowerState
{
    public DIdleS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
        if(triggerCalledEnd)
        {
            if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.dMeleeS);
            else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.dMoveS);
            else if (tower.nearestREnemy != null) towerFSM.ChangeState(tower.fsmLibrary.dRangeS);
        }
    }
}
