using UnityEngine;
using System.Collections;

public class Fly : Projectile
{
    float mTimeElapsed = 0.0f;

    void FixedUpdate()
    {
        if (mIsUsingTimeLimit)
        {
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
