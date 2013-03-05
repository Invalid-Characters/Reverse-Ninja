using UnityEngine;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public static bool IsFading = false;

    public float mFadeDuration = 1.0f;
    public float mSmoothness = 0.01f;

    Material mMaterial = null;
    float mFadeValue = 0.0f;
    float mTimeElapsed = 0.0f;

    void Awake()
    {
        mMaterial = this.renderer.material;
        mFadeValue = (1.0f * mSmoothness) / mFadeDuration;
    }

    void FixedUpdate()
    {
        if (IsFading)
        {
            mTimeElapsed += Time.deltaTime;

            if (mTimeElapsed >= mSmoothness)
            {
                mTimeElapsed = 0.0f;

                mMaterial.color = new Color(
                    mMaterial.color.r,
                    mMaterial.color.g,
                    mMaterial.color.b,
                    (mMaterial.color.a + mFadeValue));

                if (mMaterial.color.a >= 1.0f)
                {
                    Application.LoadLevel(1);
                    IsFading = false;
                }
            }
        }
    }
}
