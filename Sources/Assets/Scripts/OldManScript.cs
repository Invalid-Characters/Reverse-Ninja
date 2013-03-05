using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class OldManScript : MonoBehaviour {

    
        public Texture mSpriteSheet = null;

        public SpriteSettings mSpriteSettings = new SpriteSettings();

    // Use this for initialization
	void Start () {
        SpriteManager.Instance.SetSpriteTexture(this.gameObject, mSpriteSheet, mSpriteSettings);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
