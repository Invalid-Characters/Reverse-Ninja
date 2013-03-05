using UnityEngine;
using System.Collections;

public class DiscussionManager : MonoBehaviour 
{
    public int NoDiscussion;

    public float duration = 4;
    public float currentTime;

    public AnimateTiledTexture oldMan;
    public AnimateTiledTexture player;

    public Material[] currentMaterials;

	// Use this for initialization
	void Start ()
	{
        oldMan = GameObject.Find("OldMan").GetComponent<AnimateTiledTexture>();
        player = GameObject.Find("Player").GetComponent<AnimateTiledTexture>();
        NoDiscussion = 1;
	    SwitchSequence();
        TutorielManager.Instance.ShowMessage("To accelerate press left : ←");
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
    }

	// Update is called once per frame
	void Update () 
    {
        if (currentTime > duration || InputManager.GetKeyDownLeft())
	    {
	        currentTime = 0;
	        NoDiscussion++;

	        if (NoDiscussion == 15)
            {
                Application.LoadLevel("GameJam");
	        }
	        else
            {
                SwitchSequence();
	        }
	    }
	}

    void SwitchSequence()
    {
        if (NoDiscussion == 2 || NoDiscussion == 4 || NoDiscussion == 6 || NoDiscussion == 13)
        {
            oldMan.mIsPaused = true;
            player.mIsPaused = false;
        }
        else
        {
            oldMan.mIsPaused = false;
            player.mIsPaused = true;
        }

        this.gameObject.renderer.material = currentMaterials[NoDiscussion - 1];
    }

}
