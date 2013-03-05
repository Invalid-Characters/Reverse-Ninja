using UnityEngine;
using System.Collections;

public class Beat : MonoBehaviour
{
    public GameObject[] mEncrage = new GameObject[2];
    public float mRotationValue = 10.0f;

    static Vector3[] FixedPosition = new Vector3[2];

    float mTimeElapsed = 0.0f, mTotalTimeElapsed = 0.0f;
    float mSmoothness = 0.01f;
    float mTranslationValue = 0.0f;
    
    void Awake()
    {
        FixedPosition[0] = mEncrage[0].transform.position;
        FixedPosition[1] = mEncrage[1].transform.position;

        this.transform.position = new Vector3(
            FixedPosition[1].x + 1.0f,
            this.transform.position.y,
            this.transform.position.z);
        
        mTranslationValue = (((FixedPosition[0].x - FixedPosition[1].x) * mSmoothness) * 0.5f);

    }

    void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), mRotationValue);

        mTimeElapsed += Time.deltaTime;
        mTotalTimeElapsed += Time.deltaTime;

        if (mTimeElapsed > mSmoothness)
        {
            mTimeElapsed = 0.0f;

            this.transform.position = new Vector3(
                (this.transform.position.x + (mTranslationValue * 10.0f)),
                this.transform.position.y,
                this.transform.position.z);

            //this.transform.localScale = mScale;
            //this.renderer.material.color = Color.white;

            if (mTotalTimeElapsed >= 0.5f)
            {
                mTranslationValue *= -1.0f;
                mTotalTimeElapsed = 0.0f;

                //this.transform.localScale = (mScale * 2.0f);
                //this.renderer.material.color = Color.red;
            }
        }
    }
}
