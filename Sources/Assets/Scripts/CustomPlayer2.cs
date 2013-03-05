using UnityEngine;
using System.Collections;

public class CustomPlayer2 : MonoBehaviour 
{
    float currentTime;
    float duration;


    public Texture mSpriteSheet = null;
    public SpriteSettings mSpriteSettings = new SpriteSettings();

    public Texture mSpriteSheet2 = null;
    public SpriteSettings mSpriteSettings2 = new SpriteSettings();

    public Texture mSpriteSheet3 = null;
    public SpriteSettings mSpriteSettings3 = new SpriteSettings();

    private int cptAction;

    private GameObject discu;
    private GameObject bck;

    void Awake()
    {

        cptAction = 0;
        duration = 20;

        discu = GameObject.Find("Discussion");
        bck = GameObject.Find("Background");
        discu.SetActive(false);

    }
    
    void Start()
    {
        SpriteManager.Instance.SetSpriteTexture(this.gameObject, mSpriteSheet, mSpriteSettings);
    }


    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if(currentTime > 2 && cptAction == 0)
        {
            cptAction++;
            SpriteManager.Instance.SetSpriteTexture(this.gameObject, mSpriteSheet2, mSpriteSettings2);

            discu.SetActive(true);
        }

        if(currentTime > 7 && cptAction == 1)
        {
            cptAction++;
            discu.SetActive(false);
            SpriteManager.Instance.SetSpriteTexture(this.gameObject, mSpriteSheet3, mSpriteSettings3);
        }

        if (cptAction == 2)
        {
            bck.transform.position = new Vector3(bck.transform.position.x + (Time.deltaTime*15), bck.transform.position.y,
                                                 bck.transform.position.z);
        }

        if(currentTime > 10 && cptAction == 2)
        {
            cptAction++;
            SpriteManager.Instance.SetSpriteTexture(this.gameObject, mSpriteSheet2, mSpriteSettings2);
        }

        if (currentTime > 15 && cptAction == 3)
        {
            discu.SetActive(true);
            discu.renderer.material = Resources.Load("whatthe") as Material;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTime > duration)
        {
            Application.LoadLevel("end-generic");
        }

    }



}
