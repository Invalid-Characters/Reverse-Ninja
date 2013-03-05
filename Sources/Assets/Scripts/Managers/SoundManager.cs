using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public readonly string TagManager = "SoundManager";

    public GameObject mSoundPrefab = null;

    readonly string SoundsDirectory = "Sounds";

    Dictionary<string, AudioSource> mSoundsList = new Dictionary<string, AudioSource>();
    public Dictionary<string, AudioSource> SoundsList
    {
        get { return mSoundsList; }
    }

    static SoundManager mInstance = null;
    public static SoundManager Instance
    {
        get { return mInstance; }
        set { mInstance = value; }
    }

    void Awake()
    {
        mInstance = this.gameObject.GetComponent<SoundManager>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag(TagManager))
        {
            string name = go.gameObject.name;

            if (!mSoundsList.ContainsKey(name))
            {
                mSoundsList.Add(name, go.GetComponent<AudioSource>());
            }
        }
    }

    void OnApplicationQuit()
    {
        mSoundsList.Clear();
    }

    public void CreateAudioSource()
    {
        object[] audioObjectList = Resources.LoadAll(SoundsDirectory, typeof(AudioClip));

        foreach (object audioObject in audioObjectList)
        {
            AudioClip audioClip = (audioObject as AudioClip);
            string name = audioClip.name.GetName();

            if (!mSoundsList.ContainsKey(name))
            {
                AudioSource source = (Instantiate(mSoundPrefab) as GameObject).AddComponent<AudioSource>();

                source.gameObject.transform.parent = this.transform;

                source.gameObject.name = name;
                source.gameObject.tag = TagManager;

                source.volume = 0.6f;
                source.playOnAwake = false;
                source.clip = audioClip;
            }
        }
    }

    public void DestroyAudioSource()
    {
        foreach (string name in mSoundsList.Keys)
        {
            if (SoundsList[name] != null)
            {
                DestroyImmediate(SoundsList[name].gameObject);
            }
        }

        mSoundsList.Clear();
    }

    public void Play(string audioName)
    {
        string name = audioName.GetName();

        if (!mSoundsList.ContainsKey(name))
        {
            Debug.LogError("There is no audio clip named " + name + ".");

            return;
        }

        Play(mSoundsList[name]);
    }

    public void Play(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void Stop(string audioName)
    {
        string name = audioName.GetName();

        if (!mSoundsList.ContainsKey(name))
        {
            Debug.LogError("There is no audio clip named " + name + ".");

            return;
        }

        Stop(mSoundsList[audioName]);
    }

    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
    }
}
