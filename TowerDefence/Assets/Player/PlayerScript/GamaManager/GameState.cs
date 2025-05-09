using UnityEngine;

public class GameState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerInputHandler input;
    protected float stateTimer;

    public GameState(PlayerStateMachine stateMachine, PlayerInputHandler input)
    {
        this.stateMachine = stateMachine;
        this.input = input;
    }



    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

}
