using UnityEngine;
using System.Collections;

public class MainMenu : GameState 
{
    public MainMenu()
    {
        mCurrentState = State.MainMenu;
    }

    protected override void EnterState()
    {
        //GameStateManager.Instance.Menu.SetActive(true);
        //GameStateManager.Instance.Game.SetActive(false);
    }

    public override void UpdateState()
    {

    }

    protected override void ExitState()
    {

    }

    public override void UpdateStateGUI()
    {
        
    }
}
