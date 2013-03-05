using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerInspector : Editor
{
    bool mIsLoadedSound = false;

    public override void OnInspectorGUI()
    {
        this.DrawDefaultInspector();
        EditorGUI.BeginChangeCheck();

        SoundManager manager = (target as SoundManager);
        mIsLoadedSound = false;

        if (manager != null)
        {
            if (SoundManager.Instance == null)
            {
                SoundManager.Instance = manager;
            }

            GameObject go = (target as GameObject);

            if (go != null)
            {
                if (go.GetComponent<AudioSource>() == null)
                {
                    LoadSounds();

                    mIsLoadedSound = true;
                }
            }
        }

        if (GUILayout.Button("Refresh"))
        {
            if (!mIsLoadedSound)
            {
                LoadSounds();
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.UpdateIfDirtyOrScript();
        }
    }

    void LoadSounds()
    {
        UnloadSounds();

        if (SoundManager.Instance != null && mIsLoadedSound)
        {
            SoundManager.Instance.CreateAudioSource();
        }
    }

    void UnloadSounds()
    {
        if (SoundManager.Instance != null)
        {
            HashSet<string> nameList = new HashSet<string>();
            GameObject[] goList = GameObject.FindGameObjectsWithTag(SoundManager.Instance.TagManager);

            foreach (GameObject go in goList)
            {
                nameList.Add(go.name);
            }
            
            if (nameList.Count != goList.Length || goList.Length == 0)
            {
                SoundManager.Instance.DestroyAudioSource();

                foreach (GameObject go in GameObject.FindGameObjectsWithTag(SoundManager.Instance.TagManager))
                {
                    DestroyImmediate(go);
                }

                mIsLoadedSound = true;
            }
        }
    }
}
