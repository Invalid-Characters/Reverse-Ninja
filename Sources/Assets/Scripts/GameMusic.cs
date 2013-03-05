using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour
{
    public static AudioClip CurrentMusic = null;

    public AudioClip mDefaultMusic = null;

    void Awake()
    {
        CurrentMusic = mDefaultMusic;
        DontDestroyOnLoad(this.gameObject);
    }
}
