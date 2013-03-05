using UnityEngine;
using System.Collections;

public class TutorielManager : MonoBehaviour
{
    private static TutorielManager mInstance;

    public static TutorielManager Instance
    {
        get
        {
            return mInstance;
        }
        set { mInstance = value; }
    }

    private string mCurrentMessage;
    private float mDuration;
    private bool mVisible;

	void Awake ()
	{
	    mInstance = this.gameObject.GetComponent<TutorielManager>();
	    mVisible = false;
    }
	
    void FixedUpdate()
    {
        if (mVisible)
        {
            if (mDuration > 10)
            {
                mVisible = false;
            }
            else
            {
                mDuration += Time.deltaTime;
            }
        }
    }

    public void ShowMessage(string pMessage)
    {
        mDuration = 0;
        mVisible = true;
        mCurrentMessage = pMessage;
    }


    void OnGUI()
    {
        if (mVisible)
        {
            GUI.Box(new Rect(Screen.width * 0.25f, Screen.height - 30, Screen.width * 0.5f, 30), mCurrentMessage);
        }
    }
}
