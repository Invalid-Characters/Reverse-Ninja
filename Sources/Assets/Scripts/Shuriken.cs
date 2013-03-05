using UnityEngine;
using System.Collections;

public class Shuriken : Projectile
{
    public float mRotationSpeed = 5.0f;

    float mTimeElapsed = 0.0f;

    void FixedUpdate()
    {
        if (mIsUsingTimeLimit)
        {
            this.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), mRotationSpeed);

            mTimeElapsed += Time.deltaTime;

            if (mTimeElapsed >= mTimeLimit)
            {
                DestroyObject(this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        DestroyObject(this.gameObject);
    }
}
