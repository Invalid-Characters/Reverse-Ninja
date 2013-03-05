using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager mInstance;
    private GameState mGameState;


    public GameObject Game;
    public GameObject Menu;

    public int GameSequenceIndex;

    public static GameStateManager Instance
    {
        get { return mInstance; }
        set { mInstance = value; }
    }
    
    public GameState GameState
    {
        get { return mGameState; }
        set { mGameState = value; }
    }
    
    void Awake()
    {
        mInstance = this.gameObject.GetComponent<GameStateManager>();
        mGameState = new ActionState();
        GameSequenceIndex = 0;

        if (Camera.main.gameObject.GetComponent<AudioSource>() != null)
        {
            Camera.main.gameObject.GetComponent<AudioSource>().clip = GameMusic.CurrentMusic;
            Camera.main.gameObject.GetComponent<AudioSource>().Play();
        }
    }

	// Use this for initialization
	void Start () 
    {

	}
	
    void OnGUI()
    {
        mGameState.UpdateStateGUI();   
    }

	// Update is called once per frame
	void Update ()
	{
	    mGameState.UpdateState();
	}
}
