using UnityEngine;
using System.Collections;

public enum OPTIONS { 
    SCENELINK = 0, 
    POPUP = 1, 
    QUIT = 2,
	SONG = 3,
	NEXT_MENU = 4
};

public class MainMenuItem : MonoBehaviour { 	
	public OPTIONS menuItemType = 0;
	public int SceneIdToLink = 0;
    public GameObject mPlayer = null;
    public Material mNextSprite = null;
	public bool hideMainMenuOnAction = true;
	public AnimationState logoAnimation = null;
	public bool animateCharacterOnAction = true;
    public AudioClip mMusic = null;
	private bool mQuit = false;
	
	public GameObject nextMainMenu = null;
	public GameObject logoToManipulate = null;
	
	private BambooMainMenu mainMenuOwner = null;
	
	public bool animateCustomObject = false;
	public GameObject customObjectToAnimate = null;
	public AnimationState customAnimationForCustomObject = null;
	
	
	// Use this for initialization
	void Start ()
	{
		if(transform.parent.parent)
			mainMenuOwner = this.transform.parent.parent.GetComponent<BambooMainMenu>();
	}
	
	// Animation for the menu item
	public void wiggle()
	{
		if(this.animation.isPlaying) 
		{
			this.animation.Stop();
		}
		this.animation.Play();
	}
	
	public void menuItemAction()
	{
		switch (menuItemType) {
        case OPTIONS.SCENELINK:
				changeScene();
			
               break;
        case OPTIONS.POPUP:
            Debug.Log ("Popup is not implemented yet");
            break;
        case OPTIONS.QUIT:
            quit();
            break;
		case OPTIONS.SONG:
            GameMusic.CurrentMusic = mMusic;
			break;
		case OPTIONS.NEXT_MENU:
			Debug.Log ("Go next");
			if(logoAnimation != null)
			{
				logoToManipulate.animation.Play(logoAnimation.name);
			}
			showNextMenu();
			
			break;
        default:
            Debug.Log ("Have you set a type for your menu item?");
            break;
        }
		
		if(animateCustomObject && customObjectToAnimate && customAnimationForCustomObject)
		{
			customObjectToAnimate.animation.Play(customAnimationForCustomObject.name);
		}
		
		if(hideMainMenuOnAction) mainMenuOwner.hideMenuFaster();
	}
	
	public void quit()
	{
		mQuit = true;
		StartCoroutine(this.coChangeScene());
	}
	
	public void changeScene()
	{
		StartCoroutine(this.coChangeScene());
	}
	
    IEnumerator coChangeScene()
    {
		
		if(animateCharacterOnAction)
		{
			if(mPlayer && mNextSprite)
			{
		        mPlayer.renderer.material = mNextSprite;
		
		        while (mPlayer.transform.position.x > -50.0f)
		        {
		            mPlayer.transform.position = new Vector3(
		                (mPlayer.transform.position.x - (Time.deltaTime * 10.0f)),
		                mPlayer.transform.position.y,
		                mPlayer.transform.position.z);
		
		            yield return new WaitForEndOfFrame();
		        }
			}
			else
				Debug.LogError("You have not specified any character or it's sprite for the animation");
		}
		
		// Move to the defined scene if applicable
		if (mQuit) Application.Quit();
		else Application.LoadLevel(SceneIdToLink);
    }
	
	public void hideLogo()
	{
		logoToManipulate.animation.Play("LogoParamsHide");
	}
	
	public void showLogo()
	{
		logoToManipulate.animation.Play("LogoParamsShow");
	}
	
	public void showNextMenu()
	{
		nextMainMenu.GetComponent<BambooMainMenu>().showMenu();
	}
	
	public void hideParentMenu()
	{
		mainMenuOwner.hideMenu();
	}
	public void hideParentMenuFaster()
	{
		mainMenuOwner.hideMenuFaster();
	}
	public void showParentMenu()
	{
		mainMenuOwner.showMenu();
	}
}
