using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundTranslate : MonoBehaviour
{
    public float mTranslationSpeed = 1.0f;

    Material mMaterial = null;
    float mTranslationValue = 1.0f;

    void Awake()
    {
        mMaterial = this.renderer.material;

        StartCoroutine(coTranslate());
    }

    IEnumerator coTranslate()
    {
        while (true)
        {
            if (BaseGame.IsEnvironmentMoving)
            {
                mTranslationValue -= (Time.deltaTime * mTranslationSpeed);

                if (mTranslationValue < 0.0f)
                {
                    mTranslationValue = 1.0f;
                }

                mMaterial.mainTextureOffset = new Vector2(mTranslationValue, 0.0f);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
