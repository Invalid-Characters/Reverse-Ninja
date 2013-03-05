using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RhythmSequence
{
    private bool mIsComplete;
    private bool mFailed;
    private List<Rhythm> mRhythms;
    private int mRhythmIndex = -1;
    
    public Rhythm CurrentRhythm
    {
        get
        {
            if (RhythmIndex == -1)
            {
                return null;
            }
            return Rhythms[RhythmIndex];
        }
    }

    public bool IsComplete
    {
        get
        {
            if (mIsComplete)
            {
                mIsComplete = false;
                return true;
            }
            return false;
        }
        set { mIsComplete = value; }
    }

    public bool HasFailed
    {
        get
        {
            if(mFailed)
            {
                mFailed = false;
                return true;
            }
            return false;
        }
        set { mFailed = value; }
    }

    public List<Rhythm> Rhythms
    {
        get { return mRhythms; }
    }

    public int RhythmIndex
    {
        get { return mRhythmIndex; }
        set { mRhythmIndex = value; }
    }

    public RhythmSequence(List<Rhythm> pListRhythm)
    {
	    mRhythms = pListRhythm;
	}
    
    public void StartRhythmSequence()
    {
        mRhythmIndex = 0;
        CurrentRhythm.Start();

        this.HasFailed = false;
        this.IsComplete = false;
    }

	public void UpdateRhythmSequence() 
    {
	    if (RhythmIndex != -1 && RhythmIndex != 0)
	    {
            CurrentRhythm.UpdateRhythm();

	        if (CurrentRhythm.Fail)
	        {
	            this.HasFailed = true;
	        }
        }

        if (CurrentRhythm.IsSuccess)
        {
            if (RhythmIndex == (Rhythms.Count - 1))
            {
                mIsComplete = true;
                mRhythmIndex = -1;
            }
            else
            {
                mRhythmIndex++;
                CurrentRhythm.Start();
            }
        }
	    else if (CurrentRhythm.CurrentRhythmState == RhythmState.End)
        {
            mFailed = true;
        }
	}

    public KeyCode GetFirstRhythmKeyCode()
    {
        return Rhythms[0].GetRhythmKeyCode();
    }
}
