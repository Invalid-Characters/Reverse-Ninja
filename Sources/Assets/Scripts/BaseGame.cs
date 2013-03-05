using UnityEngine;
using System.Collections;
using System.Text;

[System.Serializable]
public class SpriteSettings
{
    public int mNbColumns = 2;
    public int mNbRows = 2;
    public float mFramesPerSecond = 10.0f;
}

public abstract class BaseGame : MonoBehaviour
{
    public static bool IsEnvironmentMoving = false;

    public int mLife = 1;

    public float mJumpForce = 100.0f;
    public float mShurikenForce = 100.0f;
    public float mNbShuriken = 1;

    protected int mStateID = -1;

    void Awake()
    {
        mStateID = -1;
    }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        if (mLife > 0)
        {
            UpdateState();
        }
    }

    protected abstract void OnStart();
    protected abstract void EnterState();
    protected abstract void SetSpriteTexture();
    protected abstract void UpdateState();
    protected abstract void ExitState();

    protected void SwitchState(int newStateID)
    {
        ExitState();
        mStateID = newStateID;

        SetSpriteTexture();
        EnterState();
    }
}

public static class Extensions
{
    public static string GetName(this string currentName)
    {
        StringBuilder name = new StringBuilder();
        string[] spaceSplit = currentName.Split(' ');

        foreach (string split in spaceSplit)
        {
            name.Append(split.ToUpper());
        }

        return name.ToString();
    }
}
