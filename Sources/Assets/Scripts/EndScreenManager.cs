using UnityEngine;
using System.Collections;

public class EndScreenManager : MonoBehaviour 
{
    float currentTime;
    float duration;


	// Use this for initialization
	void Start () {
	
	}

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;



    }

	// Update is called once per frame
	void Update () 
    {

        if (currentTime > duration)
        {
            //Application.LoadLevel(
        }

	}
}
