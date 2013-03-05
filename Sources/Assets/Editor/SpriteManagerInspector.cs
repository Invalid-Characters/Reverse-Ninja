using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpriteManager))]
public class SpriteManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        this.DrawDefaultInspector();
        EditorGUI.BeginChangeCheck();

        SpriteManager manager = (this.target as SpriteManager);

        if (manager != null)
        {
            if (SpriteManager.Instance == null)
            {
                SpriteManager.Instance = manager;
            }

            SpriteManager.Instance.SpriteList.Clear();
            LoadSprite();

            foreach (string name in SpriteManager.Instance.SpriteList.Keys)
            {
                EditorGUILayout.LabelField(name);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.UpdateIfDirtyOrScript();
        }
    }

    void LoadSprite()
    {
        if (SpriteManager.Instance != null)
        {
            SpriteManager.Instance.LoadSprite();
        }
    }
}
