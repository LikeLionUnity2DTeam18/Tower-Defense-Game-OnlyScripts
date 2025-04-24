using UnityEngine;

public class DeerIdleStandS : DeerStandS
{
    public DeerIdleStandS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

}


public class DeerIdleSitS : DeerSitS
{
    public DeerIdleSitS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.deerAttackSitS);
        else if (tower.nearestEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.deerMoveS);
        else if (tower.nearestREnemy != null) towerFSM.ChangeState(tower.fsmLibrary.deerAttackStandS);
    }
        
    public override void Exit()
    {
        base.Exit();
    }
}
