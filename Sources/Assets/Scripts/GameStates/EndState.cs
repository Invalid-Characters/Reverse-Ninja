using UnityEngine;
using System.Collections;

public class EndState : GameState  
{
    public EndState()
    {
        mCurrentState = State.EndState;
    }

    protected override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateStateGUI()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        throw new System.NotImplementedException();
    }
}
