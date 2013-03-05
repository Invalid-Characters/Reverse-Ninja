using UnityEngine;
using System.Collections;

public class introText : MonoBehaviour {
	
	public Color color = Color.black;
	
	// Use this for initialization
	void Start ()
	{
		this.renderer.material.SetColor("_Color", color);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
