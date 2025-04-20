using UnityEngine;

public class TowerFSM
{
    public TowerState currentState { get; private set; }


    public void Init(TowerState currentState)
    {
        this.currentState = currentState;
        this.currentState.Enter();
    }

    public void ChangeState(TowerState currentState)
    {
        this.currentState.Exit();
        this.currentState = currentState;
        this.currentState.Enter();
    }
}
