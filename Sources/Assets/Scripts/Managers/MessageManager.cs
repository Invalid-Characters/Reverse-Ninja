using UnityEngine;
using System.Collections;

public class MessageManager : MonoBehaviour
{
    public float Duration;
    public float ElapsedTime;
    public Material UpMaterial;
    public Material DownMaterial;
    public Material LeftMaterial;
    public Material RightMaterial;

    public Material UpFailMaterial;
    public Material DownFailMaterial;
    public Material LeftFailMaterial;
    public Material RightFailMaterial;

    public bool IsActive;

    private static MessageManager mInstance = null;
    public static MessageManager Instance
    {
        get 
        {     
            return mInstance; 
        }
        set { mInstance = value; }
    }

    void Awake()
    {
        mInstance = this.gameObject.GetComponent<MessageManager>();
        IsActive = false;
        this.gameObject.SetActive(false);
        ElapsedTime = 0;
    }

	// Use this for initialization
	void Start () 
    {

	}

    void FixedUpdate()
    {
        if (IsActive)
        {
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime > Duration)
            {
                IsActive = false;
                this.gameObject.SetActive(false);
                ElapsedTime = 0;
            }
        }
    }

    public void ChangeMessage(RhythmType pRhythmType, float pDuration, bool pIsFail)
    {
        ElapsedTime = 0;
        Duration = pDuration;
        IsActive = true;
        this.gameObject.SetActive(true);

        switch (pRhythmType)
        {
            case RhythmType.Up:
                if (!pIsFail)
                    this.gameObject.renderer.material = UpMaterial;
                else
                    this.gameObject.renderer.material = UpFailMaterial;
                break;
            case RhythmType.Down:
                if (!pIsFail)
                    this.gameObject.renderer.material = DownMaterial;
                else
                    this.gameObject.renderer.material = DownFailMaterial;
                break;
            case RhythmType.Left:
                if (!pIsFail)
                    this.gameObject.renderer.material = LeftMaterial;
                else
                    this.gameObject.renderer.material = LeftFailMaterial;
                break;
            case RhythmType.Right:
                if (!pIsFail)
                    this.gameObject.renderer.material = RightMaterial;
                else
                    this.gameObject.renderer.material = RightFailMaterial;
                break;
            default:
                break;
        }
    }
}
