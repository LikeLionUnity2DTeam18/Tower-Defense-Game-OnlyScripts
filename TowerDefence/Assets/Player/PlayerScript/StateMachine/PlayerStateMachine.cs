using UnityEngine;

public class PlayerStateMachine
{
    PlayerState currentState;

    public void Initialize( PlayerState state )
    {
        currentState = state;

    }

    public void ChangeState()
    {

    }
}
