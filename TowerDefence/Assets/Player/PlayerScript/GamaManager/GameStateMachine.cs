using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public GameState currentState { get; private set; }

    public void Initialize(GameState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(GameState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void Update()
    {
        currentState.Update();
    }


}
