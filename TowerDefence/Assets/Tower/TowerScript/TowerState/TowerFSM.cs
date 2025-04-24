using UnityEngine;

public class TowerFSM
{
    public TowerState currentState { get; private set; }
    public TowerState previousState { get; private set; }


    public void Init(TowerState currentState)
    {
        this.currentState = currentState;
        this.currentState.Enter();
    }

    public void ChangeState(TowerState currentState)
    {
        this.previousState = this.currentState;
        this.currentState = currentState;
        this.previousState.Exit();
        this.currentState.Enter();
    }
}
