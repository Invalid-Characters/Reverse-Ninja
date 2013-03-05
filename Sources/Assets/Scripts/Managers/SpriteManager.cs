using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    readonly string SpritesDirectory = "Sprites";

    static SpriteManager mInstance = null;
    public static SpriteManager Instance
    {
        get { return mInstance; }
        set { mInstance = value; }
    }

    Dictionary<string, Texture> mSpriteList = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> SpriteList
    {
        get { return mSpriteList; }
    }

    void Awake()
    {
        mInstance = this.gameObject.GetComponent<SpriteManager>();
    }

    public void LoadSprite()
    {
        object[] spriteObjectList = Resources.LoadAll(SpritesDirectory, typeof(Texture));

        foreach (object spriteObject in spriteObjectList)
        {
            Texture texture = (spriteObject as Texture);
            string name = texture.name.GetName();

            if (!mSpriteList.ContainsKey(name))
            {
                mSpriteList.Add(name, texture);
            }
        }
    }

    public void SetSpriteTexture(GameObject sprite, string textureName, SpriteSettings spriteSettings)
    {
        if (!mSpriteList.ContainsKey(textureName))
        {
            Debug.LogError("There is no sprite texture named " + textureName + ".");
            return;
        }

        SetSpriteTexture(sprite, mSpriteList[textureName], spriteSettings);
    }

    public void SetSpriteTexture(GameObject sprite, Texture texture, SpriteSettings spriteSettings)
    {
        AnimateTiledTexture tiledTexture = sprite.GetComponent<AnimateTiledTexture>();

        if (tiledTexture == null)
        {
            Debug.LogError("There is no AnimateTiledTexture script attached to the game object.");

            return;
        }

        tiledTexture.mColumns = spriteSettings.mNbColumns;
        tiledTexture.mRows = spriteSettings.mNbRows;
        tiledTexture.mFramesPerSecond = spriteSettings.mFramesPerSecond;

        sprite.renderer.material.mainTexture = texture;

        sprite.GetComponent<AnimateTiledTexture>().mIsPaused = false;
    }

    public void RestartFrame(GameObject sprite, int nbRestart, int atFrame)
    {
        sprite.GetComponent<AnimateTiledTexture>().mIsPaused = false;
        sprite.GetComponent<AnimateTiledTexture>().RestartFrame(nbRestart, atFrame);
    }

    public bool IsPaused(GameObject sprite)
    {
        return sprite.GetComponent<AnimateTiledTexture>().mIsPaused;
    }
}
