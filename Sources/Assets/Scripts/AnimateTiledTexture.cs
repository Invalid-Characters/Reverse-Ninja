using UnityEngine;
using System.Collections;

public class AnimateTiledTexture : MonoBehaviour
{
    public int mColumns = 2;
    public int mRows = 2;
    public float mFramesPerSecond = 10f;
    public bool mIsPaused = false;

    int mNbRestart = 0;
    int mAtFrame = 0;
    bool mIsRestartFrame = false;

    int mIndex = 0;
    public int Index
    {
        get { return mIndex; }
    }

    void Start()
    {
        StartCoroutine(updateTiling());

        Vector2 size = new Vector2((1.0f / mColumns), (1.0f / mRows));

        renderer.sharedMaterial.SetTextureScale("_MainTex", size);
    }

    public void RestartFrame(int nbRestart, int atFrame)
    {
        mNbRestart = nbRestart;
        mAtFrame = atFrame;
        mIndex = 0;

        mIsRestartFrame = true;
    }

    IEnumerator updateTiling()
    {
        while (true)
        {
            if (!mIsPaused)
            {
                mIndex++;

                if (mIndex >= (mRows * mColumns))
                {
                    mIndex = 0;
                }

                if (mIsRestartFrame)
                {
                    if (mIndex == mAtFrame)
                    {
                        mIndex = 0;
                        mNbRestart--;
                    }

                    if (mNbRestart == 0)
                    {
                        mIsRestartFrame = false;
                        mIsPaused = true;

                        mIndex = mAtFrame;
                    }
                }

                Vector2 offset = new Vector2(
                    (((float)mIndex / mColumns) - (mIndex / mColumns)),
                    ((mIndex / mColumns) / (float)mRows));

                renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
            }

            yield return new WaitForSeconds(1.0f / mFramesPerSecond);
        }
    }
}
