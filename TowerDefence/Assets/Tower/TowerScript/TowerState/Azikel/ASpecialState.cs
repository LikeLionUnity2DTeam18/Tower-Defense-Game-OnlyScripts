using UnityEngine;

public class ASpecialState : TSpecialState
{
    protected Azikel azikel => tower as Azikel;
    public ASpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalled2)
        {
            if (tower.nearestREnemy != null) azikel.Special();
            else if (tower.nearestMEnemy != null) azikel.Special();
            else if (tower.nearestEnemy != null) azikel.Special();
            triggerCalled2 = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
