using System.Timers;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RhythmType
{
    Up = 0, Down = 1, Left = 2, Right = 3
};

public enum RhythmState
{
    Init = 0, Update = 1, End = 2 
};

public class Rhythm
{    
    private float mElapsedTime;
    private RhythmType mCurrentRhythmType;
    private float mBeginRhythm;
    private float mEndRhythm;
    private string mSound;
    private bool mFail;
    private RhythmState mCurrentRhythmState;
    private bool mIsSuccess;

    public RhythmType CurrentRhythmType
    {
        get { return mCurrentRhythmType; }
        set { mCurrentRhythmType = value; }
    }

    public RhythmState CurrentRhythmState
    {
        get
        {
            if (mCurrentRhythmState == RhythmState.End)
            {
                mCurrentRhythmState = RhythmState.Init;
                return RhythmState.End;
            }
            return mCurrentRhythmState;
        }
        set { mCurrentRhythmState = value; }
    }

    public bool IsSuccess
    {
        get
        {
            if (mIsSuccess)
            {
                mIsSuccess = false;
                return true;
            }
            return false;
        }
        set { mIsSuccess = value; }
    }

    public bool Fail
    {
        get { return mFail; }
        set
        {
            mFail = value;
        }
    }

    public bool IsRhythmSuccess()
    {
        return mIsSuccess;
    }

    public Rhythm(RhythmType pRhythmType, float pBeginRhythm, float pEndRhythm, string pSound)
    {
        mCurrentRhythmState = RhythmState.Init;
	    CurrentRhythmType = pRhythmType;
        mBeginRhythm = pBeginRhythm;
        mEndRhythm = pEndRhythm;
        mSound = pSound;
	}

    public void Start()
    {
        mElapsedTime = 0;
        mFail = false;
        mCurrentRhythmState = RhythmState.Init;

        if (mBeginRhythm == 0)
        {
            SoundManager.Instance.Play(mSound);
            mIsSuccess = true;
            mCurrentRhythmState = RhythmState.End;
        }
    }

    public void BeginRhythm()
    {
        mFail = false;
        mCurrentRhythmState = RhythmState.Update;
    }

	public void UpdateRhythm() 
    {
        if (mCurrentRhythmState == RhythmState.Init)
        {
	        UpdateInitState();
        }
        if (mCurrentRhythmState == RhythmState.Update)
        {
            UpdateUpdateState();
        }
	}

    private void UpdateUpdateState()
    {
        if (mElapsedTime > mEndRhythm)
        {
            EndRhythm();
        }

        else
        {
            KeyCode currentKeyCode = GetRhythmKeyCode();
            ManageKeyPressed(currentKeyCode, false);
        }
    }

    private void UpdateInitState()
    {
        KeyCode currentKeyCode = GetRhythmKeyCode();
        ManageKeyPressed(currentKeyCode, true);

        if (mElapsedTime > mBeginRhythm)
        {
            BeginRhythm();
        }
    }

    private void ManageKeyPressed(KeyCode pKeyCode, bool pIsInit)
    {
        if (pIsInit)
        {
            if (InputManager.GetKeyDownUp() && pKeyCode != KeyCode.UpArrow)
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Up, 0.5f, true);
                SoundManager.Instance.Play(RhythmFactory.UpAudioClip);
                IsSuccess = false;
                Fail = true;
            }
            else if (InputManager.GetKeyDownDown() && pKeyCode != KeyCode.DownArrow)
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Up, 0.5f, true);
                SoundManager.Instance.Play(RhythmFactory.DownAudioClip);
                IsSuccess = false;
                Fail = true;
            }
            else if (InputManager.GetKeyDownLeft() && pKeyCode != KeyCode.LeftArrow)
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Left, 0.5f, true);
                SoundManager.Instance.Play(RhythmFactory.LeftAudioClip);
                IsSuccess = false;
                Fail = true;
            }
            else if (InputManager.GetKeyDownRight() && pKeyCode != KeyCode.RightArrow)
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Right, 0.5f, true);
                SoundManager.Instance.Play(RhythmFactory.RightAudioClip);
                IsSuccess = false;
                Fail = true;
            }
        }
        else
        {
            if (InputManager.GetKeyDownUp())
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Up, 0.5f, true);
                SoundManager.Instance.Play(RhythmFactory.UpAudioClip);
                if (pKeyCode == KeyCode.UpArrow)
                {
                    IsSuccess = true;
                }
                else
                {
                    IsSuccess = false;
                    Fail = true;
                }
            }
            else if (InputManager.GetKeyDownDown())
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Up, 0.5f, true);
                SoundManager.Instance.Play(RhythmFactory.DownAudioClip);
                if (pKeyCode == KeyCode.DownArrow)
                {
                    IsSuccess = true;
                }
                else
                {
                    IsSuccess = false;
                    Fail = true;
                }
            }
            else if (InputManager.GetKeyDownLeft())
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Left, 0.5f, true);
                SoundManager.Instance.Play(RhythmFactory.LeftAudioClip);
                if (pKeyCode == KeyCode.LeftArrow)
                {
                    IsSuccess = true;
                }
                else
                {
                    IsSuccess = false;
                    Fail = true;
                }
            }
            else if (InputManager.GetKeyDownRight())
            {
                MessageManager.Instance.ChangeMessage(RhythmType.Right, 0.5f, true);
                SoundManager.Instance.Play(RhythmFactory.RightAudioClip);
                if (pKeyCode == KeyCode.RightArrow)
                {
                    IsSuccess = true;
                }
                else
                {
                    IsSuccess = false;
                    Fail = true;
                }
            }
        }
    }

    public KeyCode GetRhythmKeyCode()
    {
        switch (mCurrentRhythmType)
        {
            case RhythmType.Down:
                return KeyCode.DownArrow;
            case RhythmType.Up:
                return KeyCode.UpArrow;
            case RhythmType.Left:
                return KeyCode.LeftArrow;
            case RhythmType.Right:
                return KeyCode.RightArrow;
        }
        return KeyCode.LeftApple;
    }

    public string GetRhythmArrow()
    {
        switch (mCurrentRhythmType)
        {
            case RhythmType.Down:
                return "↓";
            case RhythmType.Up:
                return "↑";
            case RhythmType.Left:
                return "←";
            case RhythmType.Right:
                return "→";
        }
        return string.Empty;
    }

    public void UpdateElapsedTime()
    {
        mElapsedTime += Time.deltaTime;
    }

    public void EndRhythm()
    {
        mCurrentRhythmState = RhythmState.End;
        mElapsedTime = 0;
    }
}
