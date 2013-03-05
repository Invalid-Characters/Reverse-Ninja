using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public static bool IsMoving = false;
    public static bool IsBossFight = false;
    public static bool IsFirstEnemy = true;
    public static float Distance = 0.0f;
    public static float TotalDistance = 0.0f;

    static int NbSpawn = 0;

    public int mMaxSpawn = 7;
    public int mStartSpawn = 1;
    public float mDistanceToSpawn = 0.75f;
    public float mSpawnDelay = 2.0f;

    readonly string EnemyPrefab = "Enemy";
    readonly string GobberPrefab = "Gobber";

    bool mIsSpawnDeactivated = false;

    void Start()
    {
        NbSpawn = mStartSpawn;
    }

    void Update()
    {
        if (IsMoving && !mIsSpawnDeactivated)
        {
            if (BaseGame.IsEnvironmentMoving)
            {
                if (mStartSpawn == 0 && NbSpawn < mMaxSpawn)
                {
                    mStartSpawn = NbSpawn;
                    NbSpawn++;
                }

                else if (mStartSpawn != 0)
                {
                    mStartSpawn--;
                }

                else
                {
                    IsBossFight = true;
                }

                StartCoroutine(coSpawn());
                mIsSpawnDeactivated = true;
            }
        }
    }

    IEnumerator coSpawn()
    {
        //while (TotalDistance == 0.0f || Distance < (TotalDistance * mDistanceToSpawn))
        //{
        //    yield return new WaitForEndOfFrame();
        //}

        yield return new WaitForSeconds(mDistanceToSpawn);

        List<GameObject> enemyList = new List<GameObject>();

        if (!IsBossFight)
        {
            for (int i = mMaxSpawn; i >= 0; i--)
            {
                if (i < NbSpawn)
                {
                    enemyList.Add((Instantiate(Resources.Load(EnemyPrefab)) as GameObject));

                    enemyList[enemyList.Count - 1].transform.parent = this.transform;
                    enemyList[enemyList.Count - 1].transform.localPosition = Vector3.zero;
                }

                yield return new WaitForSeconds(mSpawnDelay);
            }

            StartCoroutine(coReactiveSpawn());
        }

        else
        {
            for (int i = (mMaxSpawn / 2); i >= 0; i--)
            {
                if (i < 1)
                {
                    enemyList.Add((Instantiate(Resources.Load(GobberPrefab)) as GameObject));

                    enemyList[enemyList.Count - 1].transform.parent = this.transform;
                    enemyList[enemyList.Count - 1].transform.localPosition = Vector3.zero;
                }

                yield return new WaitForSeconds(mSpawnDelay);
            }
        }
    }


    IEnumerator coReactiveSpawn()
    {
        while (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
        {
            yield return new WaitForEndOfFrame();
        }

        mIsSpawnDeactivated = false;
    }
}
