using UnityEngine;
using System.Collections;

public class LifeIncrease : MonoBehaviour 
{
	void Start () 
    {
        this.rigidbody.AddForce((Vector3.right * 1000));
	}
	
	void Update () 
    {
        if (this.gameObject.transform.position.x > 70)
        {
            Destroy(this.gameObject);
        }
	}
}
