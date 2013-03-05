using System;
using UnityEngine;
using System.Collections;

public abstract class GameState
{
    public enum State
    {
        MainMenu = 0,
        IntroScreen = 1,
        ActionState = 2,
        MovingState = 3,
        BossState = 4,
        EndState = 5,
        LoseState = 6,
        UpgradeState = 7
    };

    protected State mCurrentState;

    public State CurrentState
    {
        get { return mCurrentState; }
    }

    void Awake()
    {
        //mStateID = 2;
    }

    void Update()
    {
    }

    protected abstract void EnterState();
    public abstract void UpdateState();
    public abstract void UpdateStateGUI();
    protected abstract void ExitState();

    public void SwitchState(GameState.State pNewState)
    {
        ExitState();

        switch (pNewState)
        {
            case State.IntroScreen:
                GameStateManager.Instance.GameState = new IntroState();
                break;
            case State.ActionState:
                GameStateManager.Instance.GameState = new ActionState();
                break;
            case State.MovingState:
                GameStateManager.Instance.GameState = new MovingState();
                break;
            case State.BossState:
                GameStateManager.Instance.GameState = new BossState();
                break;
            case State.EndState:
                GameStateManager.Instance.GameState = new EndState();
                break;
            case State.UpgradeState:
                GameStateManager.Instance.GameState = new UpgradeState();
                break;
        }

        EnterState();
    }
}
