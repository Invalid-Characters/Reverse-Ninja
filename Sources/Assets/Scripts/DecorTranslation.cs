using UnityEngine;
using System.Collections;

public class DecorTranslation : MonoBehaviour
{
    public float mTranslationSpeed = 1.0f;
    public Vector2 mRandomTweakSpeed = Vector2.zero;
    public ParticleEmitter mFire = null;

    const float XLimit = 90.0f;

    float mZPosition = 0.0f;

    void Awake()
    {
        mTranslationSpeed *= Random.Range(mRandomTweakSpeed.x, mRandomTweakSpeed.y);
        mZPosition = (Random.value <= 0.15f ? -1.85f : this.transform.localPosition.z);

        StartCoroutine(coTranslate());
    }

    IEnumerator coTranslate()
    {
        while (true)
        {
            if (BaseGame.IsEnvironmentMoving)
            {
                this.transform.localPosition = new Vector3(
                    (this.transform.localPosition.x + (Time.deltaTime * mTranslationSpeed)),
                    this.transform.localPosition.y,
                    mZPosition);

                if (this.transform.position.x >= XLimit)
                {
                    DestroyObject(this.gameObject);

                    yield break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
