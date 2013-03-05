using UnityEngine;
using System.Collections;

public class TextureResize : MonoBehaviour {

	public int textureX;
	public int textureY;
	public double ratio;
	public double resizeValue = 5.0;

	// Use this for initialization
	void Start () {
		
		textureX = (int) this.renderer.material.mainTexture.width * (int) resizeValue;
		textureY = (int) this.renderer.material.mainTexture.height * (int) resizeValue;
		
		ratio = textureX/textureY;
		
		this.transform.localScale = new Vector3((float) (ratio * resizeValue), (float)(1.0 * resizeValue), (float)0.0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
