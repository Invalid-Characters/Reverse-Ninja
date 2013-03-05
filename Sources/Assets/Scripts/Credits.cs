using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour
{
    bool mIsCoroutineCalled = false;

    void Update()
    {
        if (!mIsCoroutineCalled)
        {
            if (this.animation.isPlaying)
            {
                StartCoroutine(coFade());
                mIsCoroutineCalled = true;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Application.LoadLevel(1);
        }
    }

    IEnumerator coFade()
    {
        while (this.animation.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        FadeScreen.IsFading = true;
    }
}
