using UnityEngine;
using System.Collections;

public class ActionState : GameState  
{
    public ActionState()
    {
        mCurrentState = State.ActionState;
    }

    protected override void EnterState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void UpdateStateGUI()
    {
    }

    protected override void ExitState()
    {
    }
}
