using UnityEngine;
using System.Collections;

public class BambooMainMenu : MonoBehaviour {
	
	public GameObject[] menuItems;
	public bool isMenuActive = true;
	public int selectedIndex = 0;
	public bool animateMainMenu = true;
	

	// Use this for initialization
	void Start () {
		
		if (this.isMenuActive)
		{
			showMenu();
		}
		else
		{
			hideMenu (true);
		}
		
		if(menuItems.Length < 1)
		{
			Debug.LogError("You should add menuItems to the MainMenuActions");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.isMenuActive)
		{
			// Up actions
			if(Input.GetButtonDown("Up"))
			{
				if (menuItems.Length != 1)
				{
					if (selectedIndex > 0)
						selectedIndex--;
					else
						selectedIndex = menuItems.Length - 1;
				}
				menuItems[selectedIndex].GetComponent<MainMenuItem>().wiggle();
			}
			
			// Down actions
			if(Input.GetButtonDown("Down"))
			{
				if (menuItems.Length != 1)
				{
					if (selectedIndex < menuItems.Length - 1)
						selectedIndex++;
					else
						selectedIndex =  0;
				}
				menuItems[selectedIndex].GetComponent<MainMenuItem>().wiggle();
			}
			
			// Action
			if(Input.GetButtonDown("Fire1"))
			{
				menuItems[selectedIndex].GetComponent<MainMenuItem>().menuItemAction();
			}
		}
	}
	
	public void showMenu()
	{
		if (animateMainMenu)
			this.animation.Play("MenuRaise");
		StartCoroutine(coShowMenu());
	}
	
	IEnumerator coShowMenu()
	{
		if(animateMainMenu)
		{
			while(this.animation.isPlaying)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		this.IsActive = true;
	}
	
	public void hideMenu(bool firstTime=false)
	{
		if(firstTime)
		{
			this.isMenuActive = false;
			// Hack MenuLower ends before 10
			this.animation["MenuLower"].time = 10;
			this.animation.Play("MenuLower");
		}
		else
		{
			this.animation.Play("MenuLower");
		}
	}
	
	public void hideMenuFaster()
	{
		this.isMenuActive = false;
		this.animation.Play("MenuLowerFaster");
	}
	
	
	public bool IsActive {
		get {
			return this.isMenuActive;
		}
		set {
			isMenuActive = value;
		}
	}	
}
