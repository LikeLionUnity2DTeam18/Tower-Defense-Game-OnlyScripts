using UnityEngine;

public class DeerAttackSitS : DeerSitS
{
    public DeerAttackSitS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy == null)
        {
            towerFSM.ChangeState(tower.fsmLibrary.deerIdleSitS);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
