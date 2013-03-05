using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecorSpawn : MonoBehaviour
{
    public List<GameObject> mDecorList = new List<GameObject>();
    public Vector2 mScaleTweakRange = Vector2.zero;
    public Vector2 mSpawnDelay = Vector2.zero;

    void Awake()
    {
        StartCoroutine(coSpawnDecor());
    }

    IEnumerator coSpawnDecor()
    {
        while (true)
        {
            if (BaseGame.IsEnvironmentMoving)
            {
                yield return new WaitForSeconds(Random.Range(mSpawnDelay.x, mSpawnDelay.y));

                SpawnDecor();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    void SpawnDecor()
    {
        GameObject decorGO = (Instantiate(mDecorList[Random.Range(0, mDecorList.Count)]) as GameObject);
        float randScale = Random.Range(mScaleTweakRange.x, mScaleTweakRange.y);

        decorGO.transform.position = new Vector3(
            this.transform.position.x, 
            decorGO.transform.position.y, 
            this.transform.position.z);

        decorGO.transform.localScale =  new Vector3(
            ((decorGO.transform.localScale.x * randScale) * (Random.value <= 0.5f ? 1.0f : -1.0f)),
            (decorGO.transform.localScale.y * randScale), 
            0.1f);
    }
}
