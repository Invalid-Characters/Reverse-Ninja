using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SystemMessage
{
    LevelUp, 
    YouAreDead, 
    XP,
    Technique,
    Reaction,
    Follow,
    ItTakes,
    LeftRight,
    Learning,
    Fight
}

public class SystemMessageManager : MonoBehaviour
{
    [System.Serializable]
    public class MessageSettings
    {
        public SystemMessage mMessage = SystemMessage.LevelUp;
        public GameObject mPrefab = null;
    }

    public List<MessageSettings> mMessageList = new List<MessageSettings>();
    public float mMessageShowDuration = 5.0f;

    static SystemMessageManager mInstance = null;
    public static SystemMessageManager Instance
    {
        get { return mInstance; }
    }

    void Awake()
    {
        mInstance = this.gameObject.GetComponent<SystemMessageManager>();
    }

    public void ShowMessage(SystemMessage message, GameObject prefabToAlign)
    {
        switch (message)
        {
            case SystemMessage.LevelUp: StartCoroutine(coShowMessage(GetMessage(message)));
                break;

            case SystemMessage.YouAreDead: StartCoroutine(coShowMessage(GetMessage(message), true));
                break;

            case SystemMessage.XP: StartCoroutine(coShowMessage(GetMessage(message), prefabToAlign.transform));
                break;

            default:StartCoroutine(coShowMessage(GetMessage(message)));
                break;
        }
    }

    GameObject GetMessage(SystemMessage message)
    {
        foreach (MessageSettings settings in mMessageList)
        {
            if (settings.mMessage == message)
            {
                return settings.mPrefab;
            }
        }

        return null;
    }

    IEnumerator coShowMessage(GameObject message, bool isEndGame = false)
    {
        message.SetActive(true);

        yield return new WaitForSeconds(mMessageShowDuration);

        message.SetActive(false);

        if (isEndGame)
        {
            Application.LoadLevel(0);
        }
    }

    IEnumerator coShowMessage(GameObject message, Transform anchor)
    {
        float timeElapsed = 0.0f;

        message.transform.position = new Vector3(
            anchor.position.x,
            (anchor.position.y + (anchor.localScale.y / 2.0f)),
            message.transform.position.z);

        message.SetActive(true);

        while (timeElapsed < 1.0f)
        {
            timeElapsed += Time.deltaTime;

            message.transform.position = new Vector3(
                message.transform.position.x,
                (message.transform.position.y + (Time.deltaTime * 3.0f)),
                message.transform.position.z);

            yield return new WaitForEndOfFrame();
        }

        message.SetActive(false);
    }
}
