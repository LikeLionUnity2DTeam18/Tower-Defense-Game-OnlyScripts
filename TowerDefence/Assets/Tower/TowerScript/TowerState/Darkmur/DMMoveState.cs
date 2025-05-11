using UnityEditor;
using UnityEngine;

public class DMMoveState : TMoveState
{
    protected Darkmur darkmur => tower as Darkmur;
    public DMMoveState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {  
        if (triggerCalled5)
        {
            triggerCalled5 = false;
            darkmur.Teleport();
            if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.meleeState);
            else if (tower.nearestEnemy == null) towerFSM.ChangeState(tower.rangeState);
        }
        else if (triggerCalled1)
        {
            if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.meleeState);
            else if (tower.nearestEnemy == null) towerFSM.ChangeState(tower.rangeState);
        }
        else if (tower.nearestEnemy != null)
        {
            tower.anim.Play("move", 0);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
