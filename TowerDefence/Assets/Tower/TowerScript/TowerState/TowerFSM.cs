using UnityEngine;

public class TowerFSM
{
    public TowerState currentState { get; private set; }


    public void Init(TowerState currentState)
    {
        this.currentState = currentState;
        currentState.Enter();
    }

    public void ChangeState(TowerState currentState)
    {
        currentState.Exit();
        this.currentState = currentState;
        currentState.Enter();
    }
}
