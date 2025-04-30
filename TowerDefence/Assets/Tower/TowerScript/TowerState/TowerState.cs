using UnityEngine;

public class TowerState
{
    protected string stateName;
    protected Tower tower;
    protected TowerFSM towerFSM;


    protected bool triggerCalledEnd;
    protected bool triggerCalledStart;
    protected bool triggerCalled;
    protected bool triggerSpecial;
    public TowerState(Tower tower,TowerFSM towerFSM, string stateName)
    {
        this.tower = tower;
        this.towerFSM = towerFSM;
        this.stateName = stateName;
    }

    public TowerState(Tower tower, TowerFSM towerFSM)
    {
        this.tower = tower;
        this.towerFSM = towerFSM;
    }


    public virtual void Enter()
    {
        if(stateName != null) tower.anim.SetBool(stateName, true);
        else return;
        Debug.Log("현재상태" + towerFSM.currentState);
        TriggerSet();
    }
    public virtual void Update()
    {
        tower.TowerStop();
    }
    public virtual void Exit()
    {
        if (stateName != null) tower.anim.SetBool(stateName, false);
        else return;
        TriggerSet();
    }

    public virtual void AnimationEndTrigger()
    {
        triggerCalledEnd = true;
    }
    public virtual void AnimationStartTrigger()
    {
        triggerCalledStart = true;
    }
    public virtual void AnimationTrigger()
    {
        triggerCalled = true;
    }
    public virtual void AnimationSpecialTrigger()
    {
        triggerSpecial = true;
    }

    private void TriggerSet()
    {
        triggerCalled = false;
        triggerCalledStart = false;
        triggerCalledEnd = false;
        triggerSpecial = false;
    }
}
