using UnityEngine;

public class TowerState
{
    protected string stateName;
    protected Tower tower;
    protected TowerFSM towerFSM;
    public TowerState(Tower tower,TowerFSM towerFSM, string stateName)
    {
        this.tower = tower;
        this.towerFSM = towerFSM;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        tower.anim.SetBool(stateName, true);
    }
    public virtual void Update()
    {
        tower.anim.SetBool(stateName, false);
    }
    public virtual void Exit()
    {
        tower.anim.SetBool(stateName, false);
    }
}
