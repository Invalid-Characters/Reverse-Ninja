using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    int mCurrentScene = 0;

    static SceneManager mInstance = null;
    public static SceneManager Instance
    {
        get { return mInstance; }
    }

    void Awake()
    {
        mInstance = this.gameObject.GetComponent<SceneManager>();

        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadNextScene()
    {
        if ((mCurrentScene + 1) > 1)
        {
            mCurrentScene = 0;
        }

        else
        {
            mCurrentScene++;
        }

        Application.LoadLevel(mCurrentScene);
        SpawnEnemy.IsFirstEnemy = true;
    }
}
